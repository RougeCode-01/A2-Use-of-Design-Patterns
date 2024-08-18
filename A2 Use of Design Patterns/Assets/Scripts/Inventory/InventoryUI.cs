using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private InventorySlotUI[] _inventorySlots;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        List<InventoryItemSO> items = _inventoryManager.GetItems();
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

    public void DisplayItemInfo(InventoryItemSO item)
    {
        _itemNameText.text = item.itemName;
        _itemDescriptionText.text = item.itemDescription;
    }
}