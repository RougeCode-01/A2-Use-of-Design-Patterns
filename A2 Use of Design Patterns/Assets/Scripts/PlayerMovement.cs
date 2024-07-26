using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Texture2D crosshairTexture;
    private Rigidbody rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleMouseRotation();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(-mouseY, mouseX, 0);
        transform.eulerAngles += rotation;
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.Log("Raycast initiated from player position");
            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);
                Debug.Log("Hit object tag: " + hit.collider.tag);
                if (hit.collider.CompareTag("Collectable"))
                {
                    Debug.Log("Interacting with collectable object: " + hit.collider.name);
                    Destroy(hit.collider.gameObject); // Simulate picking up the object
                }
                else
                {
                    Debug.Log("Hit object is not collectable");
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object");
            }
        }
    }

    private void OnGUI()
    {
        if (crosshairTexture != null)
        {
            float xMin = (Screen.width / 2) - (crosshairTexture.width / 2);
            float yMin = (Screen.height / 2) - (crosshairTexture.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairTexture.width, crosshairTexture.height), crosshairTexture);
        }
    }
}