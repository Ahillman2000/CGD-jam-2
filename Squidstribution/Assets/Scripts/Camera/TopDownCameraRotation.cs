using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private float rotateSpeed = 100f;

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
