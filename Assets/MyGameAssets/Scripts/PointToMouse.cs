using UnityEngine;
using UnityEngine.UIElements;

public class PointToMouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure the object stays on the 2D plane
        transform.position = mousePos;
    }
}
