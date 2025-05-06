using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Item Combination")]
public class ItemCombination : ScriptableObject
{
    public ItemsScriptables itemA;  // O primeiro item da combinação
    public ItemsScriptables itemB;  // O segundo item da combinação
    public ItemsScriptables resultado; // O item resultado da fusão
}
