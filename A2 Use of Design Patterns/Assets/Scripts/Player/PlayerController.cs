using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; // Player movement speed
    [SerializeField] private Texture2D _crosshairTexture; // Crosshair texture
    [SerializeField] private InventoryUI _inventoryUI; // Reference to the Inventory UI
    [SerializeField] private TMP_Text _selectedItemText; // UI Text to display selected item
    private Rigidbody _rb; // Rigidbody component
    private Camera _mainCamera; // Main camera reference
    private List<InventoryItemSO> _inventory = new List<InventoryItemSO>(); // List to store items
    private int _selectedItemIndex = 0; // Index of the currently selected item

    // Public property to access inventory
    public List<InventoryItemSO> Inventory => _inventory;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>(); // Get Rigidbody component
        _mainCamera = Camera.main; // Get main camera
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
        UpdateSelectedItemUI(); // Update selected item UI
    }

    private void FixedUpdate()
    {
        HandleMovement(); // Handle player movement
    }

    private void Update()
    {
        HandleMouseRotation(); // Handle mouse rotation
        HandleInteraction(); // Handle player interaction
        UseItem(); // Handle item usage
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get horizontal input
        float vertical = Input.GetAxis("Vertical"); // Get vertical input

        Vector3 movement = new Vector3(horizontal, 0, vertical) * _speed * Time.deltaTime; // Calculate movement vector
        Vector3 newPosition = _rb.position + _rb.transform.TransformDirection(movement); // Calculate new position
        _rb.MovePosition(newPosition); // Move player to new position
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X"); // Get mouse X input
        float mouseY = Input.GetAxis("Mouse Y"); // Get mouse Y input

        Vector3 rotation = new Vector3(-mouseY, mouseX, 0); // Calculate rotation vector
        transform.eulerAngles += rotation; // Apply rotation to player
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Check if 'E' key is pressed
        {
            Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward); // Create a ray from the camera
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 2f, Color.red, 1f); // Draw debug ray
            if (Physics.Raycast(ray, out RaycastHit hit, 2f)) // Perform raycast
            {
                if (hit.collider.CompareTag("Collectable")) // Check if hit object is collectable
                {
                    ItemData itemData = hit.collider.GetComponent<ItemData>(); // Get ItemData component
                    if (itemData != null) // Check if ItemData is not null
                    {
                        itemData.SetInventoryUI(_inventoryUI); // Set Inventory UI
                        itemData.PickUpItem(); // Pick up item
                        _inventory.Add(itemData.GetItemData()); // Add item to inventory
                        UpdateSelectedItemUI(); // Update selected item UI
                    }
                }
            }
        }
    }

    private void UseItem()
    {
        // Use scroll wheel to switch between items
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            _selectedItemIndex = (_selectedItemIndex + 1) % _inventory.Count; // Increment selected item index
            UpdateSelectedItemUI(); // Update selected item UI
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            _selectedItemIndex = (_selectedItemIndex - 1 + _inventory.Count) % _inventory.Count; // Decrement selected item index
            UpdateSelectedItemUI(); // Update selected item UI
        }

        // Use the selected item when 'E' is pressed
        if (Input.GetKeyDown(KeyCode.E) && _inventory.Count > 0)
        {
            Debug.Log("Used item: " + _inventory[_selectedItemIndex].itemName); // Log used item
            // Implement item usage logic here
        }
    }
    
    public InventoryItemSO GetSelectedItem()
    {
        if (_inventory.Count > 0) // Check if inventory is not empty
        {
            return _inventory[_selectedItemIndex]; // Return selected item
        }
        return null; // Return null if inventory is empty
    }

    private void UpdateSelectedItemUI()
    {
        if (_inventory.Count > 0) // Check if inventory is not empty
        {
            _selectedItemText.text = "Selected Item: " + _inventory[_selectedItemIndex].itemName; // Update selected item text
        }
        else
        {
            _selectedItemText.text = "No items in inventory"; // Display no items message
        }
    }

    public void RemoveItemFromInventory(InventoryItemSO item)
    {
        if (_inventory.Contains(item)) // Check if item is in inventory
        {
            _inventory.Remove(item); // Remove item from inventory
            UpdateSelectedItemUI(); // Update selected item UI
        }
    }

    private void OnGUI()
    {
        if (_crosshairTexture != null) // Check if crosshair texture is set
        {
            float xMin = (Screen.width / 2) - (_crosshairTexture.width / 2); // Calculate x position
            float yMin = (Screen.height / 2) - (_crosshairTexture.height / 2); // Calculate y position
            GUI.DrawTexture(new Rect(xMin, yMin, _crosshairTexture.width, _crosshairTexture.height), _crosshairTexture); // Draw crosshair texture
        }
    }
}