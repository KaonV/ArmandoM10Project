using UnityEngine;

public class CraftarBotao : MonoBehaviour
{
    [Header("Referências de Sistema")]
    public CauldronDropZone cauldron;            // Seu componente que guarda os slots do caldeirão
    public CombinationManager combinationManager;  // ScriptableObject com todas as combinações
    public RecipeBook recipeBook;          // Componente que gerencia o Livro de Receitas

    [Header("Configuração de Exclusão")]
    [Tooltip("Arraste aqui o ScriptableObject MacarraoDark para que essa receita NÃO seja registrada")]
    public ItemsScriptables excludedRecipe;      // Receitas que não devem ir pro livro

    [Header("Instanciação do Resultado")]
    public Transform resultadoSpawnPoint; // Onde o novo item vai aparecer
    public CaldeiraoSlot caldeiraoSlot;       // (opcional, se você usa em outro contexto)

    /// <summary>
    /// Chamado pelo botão de Craft no Inspector (OnClick)
    /// </summary>
    public void Craftar()
    {
        // 1) Pega os itens dos slots
        var slots = cauldron.slots;
        if (slots.Length < 1 || slots[0].itemOcupando == null)
        {
            Debug.Log("Nenhum item no slot 1.");
            return;
        }

        var itemA = slots[0].itemOcupando.itemsScriptables;
        var itemB = (slots.Length > 1 && slots[1].itemOcupando != null)
                    ? slots[1].itemOcupando.itemsScriptables
                    : null;

        // 2) Verifica combinação
        var resultado = combinationManager.VerificarCombinacao(itemA, itemB);
        if (resultado == null && combinationManager.receitaPadrao != null)
        {
            resultado = combinationManager.receitaPadrao;
            Debug.Log("Combinação inválida. Usando receita padrão.");
        }

        // 3) Se houver resultado, limpa slots, instancia item e registra na UI
        if (resultado != null)
        {
            // Limpa os slots usados
            slots[0].Limpar();
            if (slots.Length > 1) slots[1].Limpar();

            // Instancia o novo item no mundo/UI
            GameObject novoItem = Instantiate(
                resultado.itemPrefab,
                resultadoSpawnPoint.position,
                Quaternion.identity,
                resultadoSpawnPoint.parent
            );
            var dragScript = novoItem.GetComponent<DragAndDrop>();
            if (dragScript != null)
            {
                dragScript.itemsScriptables = resultado;
                dragScript.podeInstanciar = false;
            }

            Debug.Log($"Resultado do craft: {resultado.nameItem}");

            // 4) Registra no livro de receitas somente se NÃO for a receita excluída
            if (resultado != excludedRecipe)
            {
                recipeBook.Register(itemA, itemB, resultado);
            }
            else
            {
                Debug.Log($"Receita {resultado.nameItem} excluída do Livro de Receitas.");
            }
        }
    }
}
