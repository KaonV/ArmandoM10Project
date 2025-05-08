using UnityEngine;

public class CaldeiraoSlot : MonoBehaviour
{
    public DragAndDrop itemOcupando;

    public bool EstaVazio => itemOcupando == null;

    // Adiciona o item ao slot
    public void AdicionarItem(DragAndDrop item)
    {
        if (itemOcupando != null && itemOcupando != item)
        {
            itemOcupando.transform.SetParent(null); 
        }


        //faz o novo item virar filho do slot na hierarquia
        itemOcupando = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
    }

    // Limpa o slot
    public void Limpar()
    {
        if (itemOcupando != null)
        {
            Debug.Log("Destruindo item: " + itemOcupando.name);
            Destroy(itemOcupando.gameObject); 
            itemOcupando = null;
        }
    }
}
