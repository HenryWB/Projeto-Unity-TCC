using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventario : MonoBehaviour
{
    [SerializeField]
    private DataBase database;

    [SerializeField]
    private GameObject slotPrefab;
    
    [SerializeField]
    private Transform inventoryPanel;

    [SerializeField]
    private List<SlotInfo> slotInfoList;

    [SerializeField]
    private int capacity;

    private string jsonString;

    private void Start()
    {
        slotInfoList = new List<SlotInfo>();

        if (PlayerPrefs.HasKey("Inventario"))
        {
            LoadSavedInventorio();
        }
        else
        {
            LoadEmptyInventorio();
        }

        gameObject.SetActive(false);
    }

    private void LoadEmptyInventorio()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, inventoryPanel);
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.database = database;
            SlotInfo newSlotInfo = newSlot.slotInfo;
            slotInfoList.Add(newSlotInfo);
        }
    }

    private void LoadSavedInventorio()
    {
        jsonString = PlayerPrefs.GetString("Inventario");
        InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(jsonString);
        this.slotInfoList = inventoryWrapper.slotInfosList;

        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, inventoryPanel);
            Slot newSlot = slot.GetComponent<Slot>();
            newSlot.SetUp(i);
            newSlot.database = database;
            newSlot.slotInfo = slotInfoList[i];
            newSlot.UpdateUI();
        }
    }

    public SlotInfo FindItemInInventorio(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && !slotInfo.isEmpty)
            {
                return slotInfo;
            }
        }

        return null;
    }

    private SlotInfo FindSuitableSlot(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && slotInfo.amount < slotInfo.maxAmount && !slotInfo.isEmpty && database.FindItemInDatabase(itemId).isStackable)
            {
                return slotInfo;
            }
        }

        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.isEmpty)
            {
                //slotInfo.EmptySlot();
                return slotInfo;
            }
        }

        return null;
    }

    private Slot FindSlot(int id)
    {
        return inventoryPanel.GetChild(id).GetComponent<Slot>();
    }

    public void AddItem(int itemId)
    {
        Item item = database.FindItemInDatabase(itemId);

        if (item != null)
        {
            SlotInfo slotInfo = FindSuitableSlot(itemId);

            if (slotInfo != null)
            {
                slotInfo.amount++;
                slotInfo.itemId = itemId;
                slotInfo.isEmpty = false;
                FindSlot(slotInfo.id).UpdateUI();
            }
        }
    }

    public void RemoveItem(int itemId)
    {
        SlotInfo slotInfo = FindItemInInventorio(itemId);
        if (slotInfo != null)
        {
            if (slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount--;
            }
            FindSlot(slotInfo.id).UpdateUI();
        }
    }
    
    public void RemoveItem(int itemId, SlotInfo slotInfo)
    {
        if (slotInfo != null)
        {
            if (slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount--;
            }
            FindSlot(slotInfo.id).UpdateUI();
        }
    }

    public void SaveInventory()
    {
        InventoryWrapper inventoryWrapper = new InventoryWrapper();
        inventoryWrapper.slotInfosList = this.slotInfoList;

        jsonString = JsonUtility.ToJson(inventoryWrapper);
        PlayerPrefs.SetString("Inventario", jsonString);
    }

    public void SwapSlots(int id_o, int id_d, Transform image_o, Transform image_d)
    {
        image_o.SetParent(inventoryPanel.GetChild(id_d));
        image_d.SetParent(inventoryPanel.GetChild(id_o));

        image_o.localPosition = Vector3.zero;
        image_d.localPosition = Vector3.zero;


        if(id_o != id_d)
        {
            SlotInfo origin = slotInfoList[id_o];
            SlotInfo destination = slotInfoList[id_d];

            slotInfoList[id_o] = destination;
            slotInfoList[id_o].id = id_o;

            slotInfoList[id_d] = origin;
            slotInfoList[id_d].id = id_d;


            Slot originSlot = inventoryPanel.GetChild(id_o).GetComponent<Slot>();
            originSlot.slotInfo = slotInfoList[id_o];
            
            Slot destinationSlot = inventoryPanel.GetChild(id_d).GetComponent<Slot>();
            destinationSlot.slotInfo = slotInfoList[id_d];

            originSlot.itemImage = image_d.GetComponent<Image>();
            destinationSlot.itemImage = image_o.GetComponent<Image>();
            
            originSlot.amountText = originSlot.itemImage.transform.GetChild(0).GetComponent<TMP_Text>();
            destinationSlot.amountText = destinationSlot.itemImage.transform.GetChild(0).GetComponent<TMP_Text>();
        }
    }

    private struct InventoryWrapper
    {
        public List<SlotInfo> slotInfosList;
    }


    [ContextMenu("Test Add - itemId = 1")]
    public void TestAdd()
    {
        AddItem(1);
    }

    [ContextMenu("Test Remove - itemId = 1")]
    public void TesteRemove()
    {
        RemoveItem(1);
    }

    [ContextMenu("Test Save")]
    public void TestSave()
    {
        SaveInventory();
    }

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
