using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraRotation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private GameObject player;
    Vector3 pos;

    void Update()
    {
        pos = new Vector3(player.transform.position.x, 60.5f, player.transform.position.z);
        transform.position = pos;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
