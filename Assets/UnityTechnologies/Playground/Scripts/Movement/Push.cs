using UnityEngine;
using UnityEngine.InputSystem; // 1. Necessary for New Input System
using System.Collections;

[AddComponentMenu("Playground/Movement/Push")]
[RequireComponent(typeof(Rigidbody2D))]
public class Push : Physics2DObject
{
    public enum PlayerIndex { Player1, Player2 }

    [Header("Input Settings")]
    public PlayerIndex playerIndex = PlayerIndex.Player1;

    [Header("Direction and strength")]
    public float pushStrength = 5f;
    public Enums.Axes axis = Enums.Axes.Y;
    public bool relativeAxis = true;

    private bool keyPressed = false;
    private Vector2 pushVector;

    // --- NEW INPUT SYSTEM VARIABLES ---
    private PlayerControls controls;
    private Rigidbody2D rb;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (playerIndex == PlayerIndex.Player1) controls.Player1.Enable();
        else controls.Player2.Enable();
    }

    private void OnDisable()
    {
        if (playerIndex == PlayerIndex.Player1) controls.Player1.Disable();
        else controls.Player2.Disable();
    }

    // Read the input from the player
    void Update()
    {
        // 2. Read the "Push" action from the appropriate map
        if (playerIndex == PlayerIndex.Player1)
            keyPressed = controls.Player1.Push.IsPressed();
        else
            keyPressed = controls.Player2.Push.IsPressed();
    }

    // FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        if(keyPressed)
        {
            pushVector = Utils.GetVectorFromAxis(axis) * pushStrength;

            // 3. Use the cached 'rb' instead of the obsolete 'rigidbody2D'
            if(relativeAxis)
            {
                rb.AddRelativeForce(pushVector);
            }
            else
            {
                rb.AddForce(pushVector);
            }
        }
    }

    // Draw an arrow to show the direction in which the object will move
    void OnDrawGizmosSelected()
    {
        if(this.enabled)
        {
            float extraAngle = (relativeAxis) ? transform.rotation.eulerAngles.z : 0f;
            pushVector = Utils.GetVectorFromAxis(axis) * pushStrength;
            Utils.DrawMoveArrowGizmo(transform.position, pushVector, extraAngle, pushStrength * .5f);
        }
    }
}