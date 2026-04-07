using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Playground/Movement/Move")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
    // We can use this enum to choose which player map to use in the Inspector
    public enum PlayerIndex { Player1, Player2 }

    [Header("Input Settings")]
    public PlayerIndex playerIndex = PlayerIndex.Player1;

    [Header("Movement")]
    public float speed = 5f;
    public Enums.MovementType movementType = Enums.MovementType.AllDirections;

    private Vector2 movement;
    private PlayerControls controls;
    private Rigidbody2D rb;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Enable only the map assigned to this specific player
        if (playerIndex == PlayerIndex.Player1)
            controls.Player1.Enable();
        else
            controls.Player2.Enable();
    }

    private void OnDisable()
    {
        if (playerIndex == PlayerIndex.Player1)
            controls.Player1.Disable();
        else
            controls.Player2.Disable();
    }

    void Update()
    {
        Vector2 moveInput;

        // Read from the correct map
        if (playerIndex == PlayerIndex.Player1)
            moveInput = controls.Player1.Move.ReadValue<Vector2>();
        else
            moveInput = controls.Player2.Move.ReadValue<Vector2>();

        float moveHorizontal = moveInput.x;
        float moveVertical = moveInput.y;

        if (movementType == Enums.MovementType.OnlyHorizontal) moveVertical = 0f;
        if (movementType == Enums.MovementType.OnlyVertical) moveHorizontal = 0f;

        movement = new Vector2(moveHorizontal, moveVertical);
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed * 10f);
    }
}