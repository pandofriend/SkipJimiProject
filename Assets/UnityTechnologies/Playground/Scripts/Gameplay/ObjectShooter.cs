using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectShooter : MonoBehaviour
{
    public enum PlayerIndex { Player1, Player2 }
    public PlayerIndex playerIndex = PlayerIndex.Player1;

    public GameObject prefabToSpawn;
    public float creationRate = .5f;
    public float shootSpeed = 5f;
    public Vector2 shootDirection = new Vector2(1f, 1f);
    public bool relativeToRotation = true;

    private float timeOfLastSpawn;
    private PlayerControls controls;
  

    
    private void Awake() => controls = new PlayerControls();

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

    void Update()
    {
        bool isShooting = false;

        // Read from the correct Player map
        if (playerIndex == PlayerIndex.Player1)
            isShooting = controls.Player1.Shoot.IsPressed();
        else
            isShooting = controls.Player2.Shoot.IsPressed();

        if (isShooting && Time.time >= timeOfLastSpawn + creationRate)
        {
            SpawnProjectile();
            timeOfLastSpawn = Time.time;
        }
    }

    void SpawnProjectile()
    {
        Vector2 actualDir = (relativeToRotation) ? (Vector2)(Quaternion.Euler(0, 0, transform.eulerAngles.z) * shootDirection) : shootDirection;
        GameObject newObject = Instantiate(prefabToSpawn, transform.position, Quaternion.Euler(0f, 0f, Utils.Angle(actualDir)));
        
        Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
        if(rb2D != null) rb2D.AddForce(actualDir * shootSpeed, ForceMode2D.Impulse);
    }
   
}