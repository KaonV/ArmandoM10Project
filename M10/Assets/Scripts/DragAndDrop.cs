using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
{

    public Image imageitem;
    [HideInInspector] public Transform parentSlot;
    public ItemsScriptables itemsScriptables;
    [SerializeField] public bool podeInstanciar = true;
    [SerializeField] private CombinationManager combinationManager;

    public void OnBeginDrag(PointerEventData eventData)
    {

        //Salva a posição do último parente do Objeto
        parentSlot = transform.parent;

        //Referência ao transform mais alto da hierarquia, objeto que não tem pai (Canvas).
        transform.SetParent(transform.root);

        //Coloca o objeto como último filho do pai atual 
        transform.SetAsLastSibling();

        imageitem.raycastTarget = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Pega a posição do mouse e move o objeto para essa posição.
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        imageitem.raycastTarget = true;
        transform.SetParent(parentSlot);

       
     

        // Instancia no mundo se puder
        if (podeInstanciar)
        {
            Vector3 mousePosition = Input.mousePosition;
            GameObject item = Instantiate(itemsScriptables.itemPrefab, mousePosition, Quaternion.identity, transform.root);
            Debug.Log("O item " + itemsScriptables.nameItem + " foi spawnado");

            item.transform.SetAsLastSibling();

            var dragScript = item.GetComponent<DragAndDrop>();
            if (dragScript != null)
                dragScript.podeInstanciar = false;
        }

      
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}



