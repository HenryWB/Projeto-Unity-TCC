using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public DataBase database;
    public Image itemImage;
    public TMP_Text amountText;
    public SlotInfo slotInfo;

    public void OnDrop(PointerEventData eventData)
    {
        DragNDrop DnD = eventData.pointerDrag.GetComponent<DragNDrop>();
        DnD.destinationSlot = this;
    }

    public void SetUp(int id)
    {
        slotInfo = new SlotInfo();
        slotInfo.id = id;
        slotInfo.EmptySlot();
    }

    public void UpdateUI()
    {
        if (slotInfo.isEmpty)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
        else
        {
            itemImage.sprite = database.FindItemInDatabase(slotInfo.itemId).itemImage;
            itemImage.enabled = true;
            if(slotInfo.amount > 1)
            {
                amountText.text = slotInfo.amount.ToString();
                amountText.gameObject.SetActive(true);
            }
            else
            {
                amountText.gameObject.SetActive(false);
            }


        }
    }
}

[System.Serializable]
public class SlotInfo
{
    public int id;
    public bool isEmpty;
    public int itemId;
    public int amount;
    public int maxAmount = 10;

    public void EmptySlot()
    {
        isEmpty = true;
        amount = 0;
        itemId = -1;
    }
}
