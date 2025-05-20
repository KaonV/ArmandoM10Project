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
            Debug.LogError("unlockSlotPrefab não está atribuído no inspector!");
            return;
        }
        if (slotsContainer == null)
        {
            Debug.LogError("slotsContainer não está atribuído no inspector!");
            return;
        }

        for (int i = 0; i < quantidadeSlots; i++)
        {
            GameObject slotGO = Instantiate(unlockSlotPrefab, slotsContainer);
            UnlockSlot slot = slotGO.GetComponent<UnlockSlot>();
            if (slot == null)
            {
                Debug.LogError("Prefab não contém o componente UnlockSlot!");
                return;
            }
            unlockSlots.Add(slot);
        }
    }

    public bool AddUnlockedItem(ItemsScriptables item)
    {
        // Primeiro, verifica se o item já está em algum slot
        foreach (var slot in unlockSlots)
        {
            if (!slot.IsEmpty() && slot.GetCurrentItem() == item)
            {
                Debug.Log($"Item {item.nameItem} já está desbloqueado.");
                return false; // Já existe, não adiciona
            }
        }

        // Se não encontrou, tenta adicionar num slot vazio
        foreach (var slot in unlockSlots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.LogWarning("Todos os slots desbloqueados estão cheios!");
        return false;
    }
}
