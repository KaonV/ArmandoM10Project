using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Combination Manager")]


public class CombinationManager : ScriptableObject
{
    public ItemCombination[] combinacoes;

    [Header("Receita padr�o")]
    public ItemsScriptables receitaPadrao;


    //Fun��o que verificar se existe a combina��o tanto de 1 item quanto de 2 itens no combination manager
    public ItemsScriptables VerificarCombinacao(ItemsScriptables a, ItemsScriptables b)
    {
        //Combina��o de um �nico item
        if (b == null)
        {
            foreach (var c in combinacoes)
            {
                if (c.itemA == a && c.itemB == null) //Combina��o com um item
                {
                    return c.resultado;
                }
            }
        }

        //Combina��o de dois itens
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
