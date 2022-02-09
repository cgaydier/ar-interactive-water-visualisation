using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Rotate_object : MonoBehaviour
{
    public GameObject objectToRotate;

    private float rotateSpeed = 0.09f;

    private float cpt = 0f; //minute touched
    private Vector2 firstPoint;
    private Vector2 secondPoint;

    // Update is called once per frame
    void Update()
    {
        if (objectToRotate.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    firstPoint = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (touch.deltaPosition.x > 10f)
                    {
                        transform.rotation = Quaternion.Euler(0f, -1 * rotateSpeed, 0f) * transform.rotation;
                    }

                    else if (touch.deltaPosition.x < 10f)
                    {
                        transform.rotation = Quaternion.Euler(0f, 1 * rotateSpeed, 0f) * transform.rotation;
                    }
                }

                return;
            }
        }
    }

}
