using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Inventario inventario;
    public Transform invetorioPanel;
    public Slot mySlot;
    public Slot destinationSlot;
    public Lixeira lixo;
    private Image myImage;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        invetorioPanel = transform.parent.parent;
        myImage = this.GetComponent<Image>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mySlot = transform.parent.GetComponent<Slot>();
        transform.SetParent(invetorioPanel);
        transform.position = eventData.position;
        myImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(destinationSlot != null)
        {
            if(destinationSlot.slotInfo.id != mySlot.slotInfo.id)
            {
                inventario.SwapSlots(mySlot.slotInfo.id, destinationSlot.slotInfo.id, this.transform, destinationSlot.itemImage.transform);
                destinationSlot.itemImage.transform.localPosition = Vector3.zero;
            }
            else
            {
                inventario.SwapSlots(mySlot.slotInfo.id, mySlot.slotInfo.id, this.transform, this.transform);
            }
        }
        else if(lixo != null)
        {
            inventario.SwapSlots(mySlot.slotInfo.id, mySlot.slotInfo.id, this.transform, this.transform);
            inventario.RemoveItem(mySlot.slotInfo.itemId, mySlot.slotInfo);
        }
        else
        {
            inventario.SwapSlots(mySlot.slotInfo.id, mySlot.slotInfo.id, this.transform, this.transform);
        }
        myImage.raycastTarget = true;
        destinationSlot = null;
    }
}
