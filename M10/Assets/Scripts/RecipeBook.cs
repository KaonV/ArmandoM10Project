using UnityEngine;
using System.Collections.Generic;
using TMPro; // ou UnityEngine.UI se usar Text

public class RecipeBook : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject entryPrefab;      // Prefab com um TMP_Text/Text
    [SerializeField] private Transform contentParent;     // Content de um ScrollView

    // HashSet pra não duplicar entradas
    private HashSet<string> discovered = new HashSet<string>();

    // Chamado sempre que descobrir uma combinação válida
    public void Register(ItemsScriptables a, ItemsScriptables b, ItemsScriptables result)
    {
        string key = GenerateKey(a, b);
        if (!discovered.Add(key))
            return; // já existe

        // Cria a linha no scroll
        var go = Instantiate(entryPrefab, contentParent);
        var label = go.GetComponentInChildren<TMP_Text>(); // ou Text
        label.text = $"{a.nameItem} + {b?.nameItem ?? ""} → {result.nameItem}";
    }

    // Gera chave canônica (ordena alfabeticamente pra ignorar ordem de slots)
    private string GenerateKey(ItemsScriptables a, ItemsScriptables b)
    {
        if (b == null)
            return a.nameItem;

        return string.Compare(a.nameItem, b.nameItem) < 0
            ? $"{a.nameItem}_{b.nameItem}"
            : $"{b.nameItem}_{a.nameItem}";
    }
}
