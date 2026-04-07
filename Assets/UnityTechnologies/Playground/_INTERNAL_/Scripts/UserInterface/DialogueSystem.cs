using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("")]
public class DialogueSystem : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Font backupFont; // Drag Arial or another font here in the Inspector!

    
    public BalloonScript CreateBalloon(string dialogueString, bool usingButton, KeyCode button, float timeToDisappear, Color backgroundC, Color textC, Transform targetObj = null)
    {
        if (balloonPrefab == null)
        {
            Debug.LogError("DialogueSystem: No balloonPrefab assigned!");
            return null;
        }

        GameObject go = GameObject.Instantiate(balloonPrefab);
        BalloonScript b = go.GetComponent<BalloonScript>();
        
        b.transform.SetParent(transform, false);
        
        
        b.Setup(dialogueString, usingButton, button, timeToDisappear, backgroundC, textC, targetObj);

        // --- FONT SAFETY CHECK ---
        Text t = go.GetComponentInChildren<Text>();
        if (t != null && t.font == null)
        {
            t.font = backupFont ?? Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        return b;
    }
}
