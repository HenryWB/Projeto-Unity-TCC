using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lixeira : MonoBehaviour, IDropHandler
{
    public SlotInfo lixeira;

    public void OnDrop(PointerEventData eventData)
    {
        DragNDrop DnD = eventData.pointerDrag.GetComponent<DragNDrop>();
        DnD.lixo = this;
    }
}
