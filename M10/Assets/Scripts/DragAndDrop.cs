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

        // Faz o raycast para detectar o que está debaixo do mouse
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            var outroItem = result.gameObject.GetComponent<DragAndDrop>();
            if (outroItem != null && outroItem != this)
            {
                var resultado = combinationManager.VerificarCombinacao(this.itemsScriptables, outroItem.itemsScriptables);
                if (resultado != null)
                {
                    // Posição média entre os dois itens
                    Vector2 mediaTela = (RectTransformUtility.WorldToScreenPoint(null, this.transform.position) +
                                         RectTransformUtility.WorldToScreenPoint(null, outroItem.transform.position)) / 2f;

                    RectTransform canvasRect = transform.root.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mediaTela, null, out Vector2 posLocal);

                    // Destroi os itens antigos
                    Destroy(this.gameObject);
                    Destroy(outroItem.gameObject);

                    // Instancia o novo item **sem parent**
                    GameObject novoItem = Instantiate(resultado.itemPrefab, transform.root);  // Não definindo parent específico
                    RectTransform novoRect = novoItem.GetComponent<RectTransform>();
                    novoRect.anchoredPosition = posLocal;  // Coloca na posição calculada

                    // Ajusta o script de arrasto
                    var dragScript = novoItem.GetComponent<DragAndDrop>();
                    if (dragScript != null)
                    {
                        dragScript.itemsScriptables = resultado;
                        dragScript.podeInstanciar = false;
                    }

                    return;
                }
            }
        }

     

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

        //transform.SetParent(parentSlot);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}



