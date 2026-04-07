using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem; // Added for New Input System

[AddComponentMenu("")]
public class BalloonScript : MonoBehaviour
{
    public Text dialogueText, buttonText;
    public UnityAction BalloonDestroyed; 

    private RectTransform rectTransform;
    private bool isUsingButton;
    private Transform targetObj;
    private float startTime;
    private float duration;

    // We create our own controls directly in the balloon!
    private PlayerControls controls;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        controls = new PlayerControls(); // Initialize the controls
    }

    private void OnEnable()
    {
        controls.Player1.Enable(); // Turn on the Player1 map
    }

    private void OnDisable()
    {
        controls.Player1.Disable();
    }

    // Notice we still accept the KeyCode here to avoid errors, but we won't use it!
    public void Setup(string dialogueString, bool _isUsingButton, KeyCode _buttonUsed, float _time, Color backgroundC, Color textC, Transform _targetObj = null)
    {
        isUsingButton = _isUsingButton;
        targetObj = _targetObj;
        duration = _time;

        GetComponent<Image>().color = backgroundC;
        dialogueText.text = dialogueString;
        dialogueText.color = textC;

        if(isUsingButton)
        {
            buttonText.text = "Press Interact"; // Custom text
            buttonText.color = textC;
        }
        else
        {
            buttonText.gameObject.SetActive(false);
            startTime = Time.time;
        }

        if(targetObj == null)
        {
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Vector3.zero);
        }
        else
        {
            rectTransform.pivot = new Vector2(0.5f, 0f);
            FollowTarget();
        }
    }

    private void Update()
    {
        if(targetObj != null) FollowTarget();

        if(isUsingButton)
        {
            // We use your 'Interact' action directly! (Make sure you created this action in your Player1 map)
            if(controls.Player1.Interact.WasPressedThisFrame())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(Time.time >= startTime + duration) Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (BalloonDestroyed != null) BalloonDestroyed();
    }

    private void FollowTarget()
    {
        Vector3 topBoundary = targetObj.position;
        SpriteRenderer sr = targetObj.GetComponent<SpriteRenderer>();
        if(sr != null) topBoundary.y += sr.bounds.size.y;
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, topBoundary);
    }
}