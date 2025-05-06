using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) 
        {
            GameObject itemDropado = eventData.pointerDrag;
            DragAndDrop itemArrastado = itemDropado.GetComponent<DragAndDrop>();
            itemArrastado.parentSlot = transform;
        }
        
    }
    
}

  

