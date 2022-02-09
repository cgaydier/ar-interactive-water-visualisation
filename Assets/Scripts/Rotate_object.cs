using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotate_object : MonoBehaviour
{
    private float rotateSpeed = 0.09f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("La");
        }
        float rotX = Input.GetAxis("Mouse X") * rotateSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotateSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }
}
