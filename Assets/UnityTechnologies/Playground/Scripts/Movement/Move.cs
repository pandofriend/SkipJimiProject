using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

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

    [System.Obsolete]
    void Update()
    {
        
       
        Vector2 moveInput;

        // Read from the correct map
        if (playerIndex == PlayerIndex.Player1)
        
    
    
            moveInput = controls.Player1.Move.ReadValue<Vector2>();
        else
            moveInput = controls.Player2.Move.ReadValue<Vector2>();

        float moveHorizontal = moveInput.x;
        float moveVertical = 0f;
        if (isDashing) {
            moveVertical = moveInput.y;
        }

        if (movementType == Enums.MovementType.OnlyHorizontal) moveVertical = 0f;
        if (movementType == Enums.MovementType.OnlyVertical) moveHorizontal = 0f;

        movement = new Vector2(moveHorizontal, moveVertical);
        if (playerIndex == PlayerIndex.Player1)
        {
            if (controls.Player1.Dash.triggered && canDash)
            {
                Vector2 dashInput = moveInput;
                StartCoroutine(Dash());
            }
        }
       // else
       // {
           // if (controls.Player2.Dash.triggered && canDash) {
               // StartCoroutine(Dash());
           // }
       // }
    }

    [System.Obsolete]
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 dashInput = controls.Player1.Move.ReadValue<Vector2>();
        Vector2 dashDirection = dashInput.normalized;
        rb.velocity = (dashDirection * dashingPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.AddForce(movement * speed * 10f);
    }
}