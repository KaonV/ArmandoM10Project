using UnityEngine;

public class CaldeiraoSlot : MonoBehaviour
{
    public DragAndDrop itemOcupando;

    public bool EstaVazio => itemOcupando == null;

    // Adiciona o item ao slot
    public void AdicionarItem(DragAndDrop item)
    {
        if (itemOcupando != null)
        {
            // Se o slot já estiver ocupado, limpamos primeiro
            Limpar();
        }

        itemOcupando = item;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
    }

    // Limpa o slot
    public void Limpar()
    {
        if (itemOcupando != null)
        {
            Destroy(itemOcupando.gameObject); // Destroi o item no slot
            itemOcupando = null; // Limpa a referência ao item
        }
    }
}
