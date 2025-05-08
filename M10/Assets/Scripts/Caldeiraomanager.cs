using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronDropZone : MonoBehaviour, IDropHandler
{
    public CaldeiraoSlot[] slots; 

    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag?.GetComponent<DragAndDrop>();
        if (item == null) return;

        foreach (var slot in slots)
        {
            if (slot.EstaVazio)
            {
                slot.AdicionarItem(item);
                return;
            }
        }

        Debug.Log("Todos os slots do caldeirão estão ocupados.");
    }

    public void Craftar(CombinationManager combinationManager)
    {
        ItemsScriptables itemA = null;
        ItemsScriptables itemB = null;

        // Acessa os itens nos slots
        if (slots[0].itemOcupando != null)
        {
            itemA = slots[0].itemOcupando.itemsScriptables;
        }

        if (slots[1].itemOcupando != null)
        {
            itemB = slots[1].itemOcupando.itemsScriptables;
        }

        // Se houver 1 item
        if (itemA != null && itemB == null)
        {
            var resultado = combinationManager.VerificarCombinacao(itemA, null);
            if (resultado != null)
            {
                // Limpar slots
                LimparSlots();

                // Instancia o resultado
                var novoItem = Instantiate(resultado.itemPrefab, transform);
                var drag = novoItem.GetComponent<DragAndDrop>();
                if (drag != null)
                {
                    drag.itemsScriptables = resultado;
                    drag.transform.SetAsLastSibling(); // Opcional: para manter no topo da UI
                }

                Debug.Log($"Combinou {itemA.nameItem} = {resultado.nameItem}");
                return;
            }
        }
        // Caso seja uma combinação de 2 itens
        else if (itemA != null && itemB != null)
        {
            var resultado = combinationManager.VerificarCombinacao(itemA, itemB);
            if (resultado != null)
            {
                // Limpar slots
                LimparSlots();

                // Instancia o resultado
                var novoItem = Instantiate(resultado.itemPrefab, transform);
                var drag = novoItem.GetComponent<DragAndDrop>();
                if (drag != null)
                {
                    drag.itemsScriptables = resultado;
                    drag.transform.SetAsLastSibling(); // Opcional: para manter no topo da UI
                }

                Debug.Log($"Combinou {itemA.nameItem} + {itemB.nameItem} = {resultado.nameItem}");
                return;
            }
        }

        Debug.Log("Combinação inválida.");
    }

    // Método para limpar os slots
    private void LimparSlots()
    {
        foreach (var slot in slots)
        {
            slot.Limpar();
        }
    }
}
