using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public Camera playerCamera;

    private CharacterController controller;
    private InputSystem_Actions controls;
    private Vector2 movementInput;



    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    public override void OnStartAuthority()
    {
        controller= GetComponent<CharacterController>();
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => movementInput = Vector2.zero;

        controls.Enable();
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Disable();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move != Vector3.zero)
        {
           transform.forward = move;
        }
    }
}
