using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private InventoryItemSO _itemData;
    private InventoryUI _inventoryUI;

    public void SetInventoryUI(InventoryUI inventoryUI)
    {
        _inventoryUI = inventoryUI;
    }

    public void PickUpItem()
    {
        if (_inventoryUI != null)
        {
            _inventoryUI.UpdateInventoryUI();
        }
        Destroy(gameObject);
    }

    public InventoryItemSO GetItemData()
    {
        return _itemData;
    }
}