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
        if (podeInstanciar)
        {
            
            eventData.pointerDrag = null;
            return;
        }

        //Relaciona o parent do objeto a variável parentSlot
        parentSlot = transform.parent;

        //Pega o transform do objeto e seta para o Pai único da hierarquia, assim tirando ele da hierarquia do slot
        //e colocando no Canvas que é o Pai mais alto.
        transform.SetParent(transform.root);

        //Seta o objeto para ficar no topo da hierarquia
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

        // Tenta encontrar um novo slot onde o item foi solto
        GameObject objetoAbaixo = eventData.pointerEnter;
        CaldeiraoSlot novoSlot = null;

        if (objetoAbaixo != null)
        {
            novoSlot = objetoAbaixo.GetComponentInParent<CaldeiraoSlot>();
        }

        if (novoSlot != null)
        {
            // Se foi solto num novo slot, adiciona o item lá
            novoSlot.AdicionarItem(this);
            parentSlot = novoSlot.transform;
        }
        else
        {
            // Se não foi solto num slot válido, limpa o antigo
            if (parentSlot != null)
            {
                CaldeiraoSlot slotAntigo = parentSlot.GetComponent<CaldeiraoSlot>();
                if (slotAntigo != null && slotAntigo.itemOcupando == this)
                {
                    slotAntigo.itemOcupando = null;
                }
            }

           
            
           
        }

    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (podeInstanciar)
        {
            // Instancia o item na posição do mouse
            GameObject copia = Instantiate(itemsScriptables.itemPrefab, Input.mousePosition, Quaternion.identity, transform.root);
            var copiaScript = copia.GetComponent<DragAndDrop>();
            if (copiaScript != null)
            {
                copiaScript.itemsScriptables = this.itemsScriptables;
                copiaScript.podeInstanciar = false;

                
             
            }
        }
    }
}



