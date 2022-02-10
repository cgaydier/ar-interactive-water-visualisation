using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateObject : MonoBehaviour
{
    private float rotateSpeed = 0.09f;

    void Update()
    {
        // Touch controls for rotation
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            // Rotate Player.  Could change Rotate to Translate to move the player.

            Vector3 newRotation = new Vector3(touchDeltaPosition.y * rotateSpeed, -touchDeltaPosition.x * rotateSpeed, 0);
            transform.Rotate(newRotation);
        }
    }
}
