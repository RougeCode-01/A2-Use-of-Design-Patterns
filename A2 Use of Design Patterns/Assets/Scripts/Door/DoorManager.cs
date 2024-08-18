using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject _itemSprite; // Sprite to indicate required item
    [SerializeField] private InventoryItemSO _requiredItem; // Required item to open the door
    [SerializeField] private GameObject _door; // Door GameObject
    [SerializeField] private PlayerController _playerController; // Reference to the PlayerController script
    private bool _isPlayerInRange = false; // Track if the player is in range

    private void Start()
    {
        _itemSprite.SetActive(false); // Hide the item sprite
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerInRange) // Check if player is interacting with the door and is in range
        {
            OpenDoor(); // Open the door
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player entered the trigger
        {
            _itemSprite.SetActive(true); // Show the item sprite
            _isPlayerInRange = true; // Player is in range
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player exited the trigger
        {
            _itemSprite.SetActive(false); // Hide the item sprite
            _isPlayerInRange = false; // Player is out of range
        }
    }

    private void OpenDoor()
    {
        InventoryItemSO selectedItem = _playerController.GetSelectedItem(); // Get the selected item from the player
        if (selectedItem != null && selectedItem == _requiredItem) // Check if the player has the right item and it is selected
        {
            _door.SetActive(false); // Deactivate the door to "open" it
            Debug.Log("Door opened!"); // Log door opened
            _playerController.RemoveItemFromInventory(_requiredItem); // Remove the item from inventory
        }
        else
        {
            Debug.Log("You need to select a " + _requiredItem.itemName + " to open this door."); // Log message if the player doesn't have the item or it is not selected
        }
    }
}