using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
{

    public Image imageitem;
    [HideInInspector] public Transform parentSlot;
    public ItemsScriptables itemsScriptables;
    [SerializeField] private bool podeInstanciar = true;

    public void OnBeginDrag(PointerEventData eventData)
    {

        //Salva a posi��o do �ltimo parente do Objeto
        parentSlot = transform.parent;

        //Refer�ncia ao transform mais alto da hierarquia, objeto que n�o tem pai (Canvas).
        transform.SetParent(transform.root);

        //Coloca o objeto como �ltimo filho do pai atual 
        transform.SetAsLastSibling();

        imageitem.raycastTarget = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Pega a posi��o do mouse e move o objeto para essa posi��o.
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (podeInstanciar)
        {
            //Pega a posi��o do mouse e armazena na vari�vel
            Vector3 mousePosition = Input.mousePosition;


            GameObject item = Instantiate(itemsScriptables.itemPrefab, mousePosition, Quaternion.identity, transform.root);
            Debug.Log("O item " + itemsScriptables.nameItem + " foi spawnado");

            item.transform.SetAsLastSibling();

            var dragScript = item.GetComponent<DragAndDrop>();
            if (dragScript != null)
                dragScript.podeInstanciar = false;

        }
      


        //Seta o parent do objeto de acordo com o parentSlot
        transform.SetParent(parentSlot);


        imageitem.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}



