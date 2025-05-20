using System.Collections.Generic;
using UnityEngine;

public class UnlockSlotsManager : MonoBehaviour
{
    public GameObject unlockSlotPrefab; // Seu prefab de slot
    public Transform slotsContainer;    // Ex: UnlockSlotsPanel transform
    public int quantidadeSlots = 10;

    private List<UnlockSlot> unlockSlots = new List<UnlockSlot>();

    void Start()
    {
        if (unlockSlotPrefab == null)
        {
            Debug.LogError("unlockSlotPrefab n�o est� atribu�do no inspector!");
            return;
        }
        if (slotsContainer == null)
        {
            Debug.LogError("slotsContainer n�o est� atribu�do no inspector!");
            return;
        }

        for (int i = 0; i < quantidadeSlots; i++)
        {
            GameObject slotGO = Instantiate(unlockSlotPrefab, slotsContainer);
            UnlockSlot slot = slotGO.GetComponent<UnlockSlot>();
            if (slot == null)
            {
                Debug.LogError("Prefab n�o cont�m o componente UnlockSlot!");
                return;
            }
            unlockSlots.Add(slot);
        }
    }

    public bool AddUnlockedItem(ItemsScriptables item)
    {
        // Primeiro, verifica se o item j� est� em algum slot
        foreach (var slot in unlockSlots)
        {
            if (!slot.IsEmpty() && slot.GetCurrentItem() == item)
            {
                Debug.Log($"Item {item.nameItem} j� est� desbloqueado.");
                return false; // J� existe, n�o adiciona
            }
        }

        // Se n�o encontrou, tenta adicionar num slot vazio
        foreach (var slot in unlockSlots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.LogWarning("Todos os slots desbloqueados est�o cheios!");
        return false;
    }
}
