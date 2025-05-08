using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Combination Manager")]


public class CombinationManager : ScriptableObject
{
    public ItemCombination[] combinacoes;

    [Header("Receita padrão")]
    public ItemsScriptables receitaPadrao;


    //Função que verificar se existe a combinação tanto de 1 item quanto de 2 itens no combination manager
    public ItemsScriptables VerificarCombinacao(ItemsScriptables a, ItemsScriptables b)
    {
        //Combinação de um único item
        if (b == null)
        {
            foreach (var c in combinacoes)
            {
                if (c.itemA == a && c.itemB == null) //Combinação com um item
                {
                    return c.resultado;
                }
            }
        }

        //Combinação de dois itens
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
