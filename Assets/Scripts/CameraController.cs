using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 mouseOriginPoint;
    private Vector3 offset;
    private bool isDragging;

    private void LateUpdate()
    {
        // Debug.Log(Input.GetAxis("Mouse ScrollWheel"));

        // Multiplying by orthographic size zooms more as the camera is further and less ass the camera is closer
        // Clamp prevents infinite zoom ability 
        Camera.main.orthographicSize = 
            Mathf.Clamp(Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (Camera.main.orthographicSize), 2.5f, 50f);

        // If middle mouse is pressed down 
        if (Input.GetMouseButton(2))
        {
            offset = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
            if (!isDragging)
            {
                isDragging = true;
                mouseOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            isDragging = false;
        }
        if (isDragging)
        {
            Camera.main.transform.position = mouseOriginPoint - offset;
        }
    }
    
}
