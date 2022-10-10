using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject Protected;
    MeshCollider objcollider;

    private void Start()
    {
        objcollider = Protected.GetComponent<MeshCollider>();
    }

    private void OnDisable()
    {
        objcollider.enabled = true;
    }
}
