using UnityEngine;
using UnityEngine.SceneManagement;  // Se for carregar uma cena de vitória
// ou usando seu próprio GameManager para win logic

public class CraftarBotao : MonoBehaviour
{
    [Header("Referências de Sistema")]
    public CauldronDropZone cauldron;
    public CombinationManager combinationManager;
    public RecipeBook recipeBook;

    [Header("Configuração de Exclusão")]
    public ItemsScriptables excludedRecipe;

    [Header("Receita de Vitória")]
    [Tooltip("Arraste aqui o ScriptableObject que, ao ser craftado, faz o jogador GANHAR")]
    public ItemsScriptables victoryRecipe;

    [Header("Instanciação do Resultado")]
    public Transform resultadoSpawnPoint;
    public CaldeiraoSlot caldeiraoSlot;

    // (Opcional) se tiver um GameManager com método WinGame()
    //public GameManager gameManager;

    public void Craftar()
    {
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

        var resultado = combinationManager.VerificarCombinacao(itemA, itemB);
        if (resultado == null && combinationManager.receitaPadrao != null)
        {
            resultado = combinationManager.receitaPadrao;
            Debug.Log("Combinação inválida. Usando receita padrão.");
        }

        if (resultado != null)
        {
            // Limpa slots
            slots[0].Limpar();
            if (slots.Length > 1) slots[1].Limpar();

            // Instancia o novo item
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

            // Registra no livro, exceto o excluído
            if (resultado != excludedRecipe)
                recipeBook.Register(itemA, itemB, resultado);

            // ** Verifica vitória **
            if (resultado == victoryRecipe)
            {
                SceneManager.LoadScene("WinScene");
                Debug.Log("=== RECEITA FINAL ALCANÇADA! VOCÊ GANHOU! ===");

                
                
            }
        }
    }
}
