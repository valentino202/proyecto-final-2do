using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : Ships
{
    public InputSystem_Actions input;
    protected override void Start()
    {
        base.Start();
    }
    private void Awake()
    {
        input = new();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.started += OnMove;
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
    }
    private void OnDisable()
    {
        input.Player.Move.started -= OnMove;
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;
        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
     

    }
    void Update()
    {
       
        //float moveX = Input.GetAxisRaw("Horizontal");
       // float moveY = Input.GetAxisRaw("Vertical");

       // moveDirection = new Vector2(moveX, moveY).normalized;
    }
  

    protected override void Die()
    {
        FindAnyObjectByType<UIGameManager>()?.TerminarJuego(false);

        base.Die();
    }
}
