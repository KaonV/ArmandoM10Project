using UnityEngine;

public class CraftarBotao : MonoBehaviour
{
    public CauldronDropZone cauldron;
    public CombinationManager combinationManager;
    public Transform resultadoSpawnPoint;
    public CaldeiraoSlot caldeiraoSlot;

    public void Craftar()
    {
        var slots = cauldron.slots;

        if (slots.Length < 1 || slots[0].itemOcupando == null)
        {
            Debug.Log("Nenhum item no slot 1.");
            return;
        }

        ItemsScriptables itemA = slots[0].itemOcupando.itemsScriptables;
        ItemsScriptables itemB = null;

        if (slots.Length > 1 && slots[1].itemOcupando != null)
            itemB = slots[1].itemOcupando.itemsScriptables;

        var resultado = combinationManager.VerificarCombinacao(itemA, itemB);

        if (resultado == null && combinationManager.receitaPadrao != null)
        {
            resultado = combinationManager.receitaPadrao;
            Debug.Log("Combinação inválida. Usando receita padrão.");
        }

        if (resultado != null)
        {
            // Limpa somente os slots 0 e 1
            if (slots[0] != null) slots[0].Limpar();
            if (slots.Length > 1 && slots[1] != null) slots[1].Limpar();

            // Instancia o novo item
            GameObject novoItem = Instantiate(resultado.itemPrefab, resultadoSpawnPoint.position, Quaternion.identity, resultadoSpawnPoint.parent);
            var dragScript = novoItem.GetComponent<DragAndDrop>();
            if (dragScript != null)
            {
                dragScript.itemsScriptables = resultado;
                dragScript.podeInstanciar = false;
            }

          

            Debug.Log($"Resultado do craft: {resultado.nameItem}");
        }
    }

}
