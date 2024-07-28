using UnityEngine;

public class OpenDoorCommand : IInteractionCommand
{
    private Door _door;
    private InventoryItemSO _key;

    public OpenDoorCommand(Door door, InventoryItemSO key)
    {
        _door = door;
        _key = key;
    }

    public void Execute()
    {
        if (_door.IsLocked && _door.Unlock(_key))
        {
            _door.Open();
        }
    }
}