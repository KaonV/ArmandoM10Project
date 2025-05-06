using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Combination Manager")]
public class CombinationManager : ScriptableObject
{
    public ItemCombination[] combinacoes;

    public ItemsScriptables VerificarCombinacao(ItemsScriptables a, ItemsScriptables b)
    {
        foreach (var c in combinacoes)
        {
            if ((c.itemA == a && c.itemB == b) || (c.itemA == b && c.itemB == a))
            {
                return c.resultado;
            }
        }
        return null;
    }
}
