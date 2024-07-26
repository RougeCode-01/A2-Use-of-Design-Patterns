using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private InventorySlotUI[] _inventorySlots;
    private List<InventoryItemSO> _items = new List<InventoryItemSO>();

    public void UpdateInventory(List<InventoryItemSO> items)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < items.Count)
            {
                _inventorySlots[i].SetItem(items[i]);
            }
            else
            {
                _inventorySlots[i].ClearSlot();
            }
        }
    }

    public void AddItem(InventoryItemSO newItem)
    {
        _items.Add(newItem);
        UpdateInventory(_items);
    }
}