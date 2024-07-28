using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLocked { get; private set; } = true;

    public bool Unlock(InventoryItemSO key)
    {
        // Check if the key is correct
        if (key.itemName == "Door Key")
        {
            IsLocked = false;
            return true;
        }
        return false;
    }

    public void Open()
    {
        if (!IsLocked)
        {
            // Open door logic
        }
    }
}