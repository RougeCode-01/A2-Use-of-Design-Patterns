using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private InventoryItemSO _item;
    private InventoryUI _inventoryUI;

    private void Start()
    {
        _inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void SetItem(InventoryItemSO newItem)
    {
        _item = newItem;
        _icon.sprite = _item.itemIcon;
        _icon.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }

    public void OnSlotClicked()
    {
        if (_item != null)
        {
            _inventoryUI.DisplayItemInfo(_item);
        }
    }
}