using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsHeliCam : MonoBehaviour
{
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private GameObject player;
    Vector3 pos;
    //[SerializeField] private Transform target;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        pos = new Vector3(player.transform.position.x, 120.82f, player.transform.position.z);
        transform.position = pos;
        //transform.LookAt(target);
    }
}
