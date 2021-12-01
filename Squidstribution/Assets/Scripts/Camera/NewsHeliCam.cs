using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsHeliCam : MonoBehaviour
{
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private float rotateSpeed = 100f;
    //[SerializeField] private Transform target;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        //transform.LookAt(target);
    }
}
