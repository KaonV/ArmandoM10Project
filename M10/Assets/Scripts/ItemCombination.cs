using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Item Combination")]
public class ItemCombination : ScriptableObject
{
    public ItemsScriptables itemA;  // O primeiro item da combina��o
    public ItemsScriptables itemB;  // O segundo item da combina��o
    public ItemsScriptables resultado; // O item resultado da fus�o
}
