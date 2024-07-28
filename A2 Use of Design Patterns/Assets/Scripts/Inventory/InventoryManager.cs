using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventoryItemSO> _items = new List<InventoryItemSO>();

    public void AddItem(InventoryItemSO newItem)
    {
        _items.Add(newItem);
        // Trigger event to update UI
    }

    public void RemoveItem(InventoryItemSO item)
    {
        _items.Remove(item);
        // Trigger event to update UI
    }

    public List<InventoryItemSO> GetItems()
    {
        return _items;
    }
}