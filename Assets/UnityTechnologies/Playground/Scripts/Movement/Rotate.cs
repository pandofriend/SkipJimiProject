using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Playground/Movement/Rotate")]
[RequireComponent(typeof(Rigidbody2D))]
public class Rotate : Physics2DObject
{
    // 1. Add the Player selection enum
    public enum PlayerIndex { Player1, Player2 }

    [Header("Input Settings")]
    public PlayerIndex playerIndex = PlayerIndex.Player1;

    [Header("Rotation")]
    public float speed = 5f;
    
    private float spin;
    private PlayerControls controls;
    private Rigidbody2D rb;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // 2. Enable the specific map for this player
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

        // 3. Read from the correct map
        if (playerIndex == PlayerIndex.Player1)
            moveInput = controls.Player1.Move.ReadValue<Vector2>();
        else
            moveInput = controls.Player2.Move.ReadValue<Vector2>();

        // We use the horizontal (X) axis for the spin
        spin = moveInput.x;
    }
    
    void FixedUpdate()
    {
        rb.AddTorque(-spin * speed);
    }
}