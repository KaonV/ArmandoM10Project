using System.Collections.Generic;
using UnityEngine;

public class UnlockSlotsManager : MonoBehaviour
{
    [Header("Prefabs & Container")]
    public GameObject unlockSlotPrefab;
    public Transform slotsContainer;

    [Header("Configuração de Paginação")]
    public int totalSlots = 20;
    public int slotsPerPage = 5;

    [Header("Itens Base (só na página 1)")]
    public ItemsScriptables[] baseItems;
    // Arraste aí no Inspector os 4 itens que devem
    // aparecer sempre na primeira página.

    private List<UnlockSlot> unlockSlots = new List<UnlockSlot>();
    private int currentPage = 0;
    private int totalPages = 0;

    void Start()
    {
        // 1) Cria todos os slots
        for (int i = 0; i < totalSlots; i++)
        {
            var go = Instantiate(unlockSlotPrefab, slotsContainer);
            var slot = go.GetComponent<UnlockSlot>();
            unlockSlots.Add(slot);
        }

        // 2) Preenche os 4 primeiros com os itens base
        //    (eles são index 0,1,2,3, e portanto ficarão na página 0)
        for (int i = 0; i < baseItems.Length && i < unlockSlots.Count; i++)
        {
            unlockSlots[i].SetItem(baseItems[i]);
        }

        // 3) Calcula total de páginas
        totalPages = Mathf.CeilToInt((float)totalSlots / slotsPerPage);

        // 4) Só agora exibe a página correta (p. ex. a 0)
        ShowPage();
    }

    private void ShowPage()
    {
        int start = currentPage * slotsPerPage;
        int end = start + slotsPerPage;

        for (int i = 0; i < unlockSlots.Count; i++)
            unlockSlots[i].gameObject.SetActive(i >= start && i < end);
    }

    public void NextPage()
    {
        currentPage = (currentPage + 1) % totalPages;
        ShowPage();
    }

    public void PrevPage()
    {
        currentPage = (currentPage - 1 + totalPages) % totalPages;
        ShowPage();
    }

    // Continua igual: usar AddUnlockedItem(item) quando desbloquear
    public bool AddUnlockedItem(ItemsScriptables item)
    {
        foreach (var slot in unlockSlots)
            if (!slot.IsEmpty() && slot.GetCurrentItem() == item)
                return false;

        foreach (var slot in unlockSlots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.LogWarning("Todos os slots estão cheios!");
        return false;
    }
}
