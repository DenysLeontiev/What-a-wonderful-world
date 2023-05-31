using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 10f;

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded; // current state whether we are grounded or not
    private Vector3 velocity; // for handling jumping and gravity(velocity.y)

    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private SelectionManager selectionManager;
    private Inventory inventory;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        inventory = new Inventory();
        inventoryUI.SetInventory(inventory); // here we get the reference inventory
        selectionManager.SetInventory(inventory); // here we get the reference inventory
    }

    private void Update()
    {
        isGrounded = IsGrounded();

        if(isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(gravity * jumpHeight * -2f);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity *Time.deltaTime);

    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
