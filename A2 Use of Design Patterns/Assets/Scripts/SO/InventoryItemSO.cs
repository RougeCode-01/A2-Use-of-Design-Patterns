using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]
public class InventoryItemSO : ScriptableObject
{
    //Idk why isnt not appearing
    public string itemName; // Name of the item
    public string itemDescription; // Description of the item
    public Sprite itemIcon; // Icon of the item
    public GameObject itemModel; // 3D model of the item
}