using UnityEngine;

public class UnlockSlot : MonoBehaviour
{
    private ItemsScriptables currentItem;
    private GameObject currentItemInstance;

    public bool IsEmpty() => currentItem == null;

    public ItemsScriptables GetCurrentItem()
    {
        return currentItem;
    }
    public void SetItem(ItemsScriptables item)
    {
        ClearItemInstance();

        currentItem = item;

        if (currentItem != null && currentItem.itemPrefab != null)
        {
            // Instancia o prefab como filho desse slot, com posição zero local
            currentItemInstance = Instantiate(currentItem.itemPrefab, transform);
            currentItemInstance.transform.localPosition = Vector3.zero;
            currentItemInstance.transform.localRotation = Quaternion.identity;
            currentItemInstance.transform.localScale = Vector3.one;
        }
    }

    private void ClearItemInstance()
    {
        if (currentItemInstance != null)
        {
            Destroy(currentItemInstance);
            currentItemInstance = null;
        }
    }

    public void Limpar()
    {
        ClearItemInstance();
        currentItem = null;
    }
}
