using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DespawnTime());
    }
    // Update is called once per frame
    IEnumerator DespawnTime()
    {
        float rand = Random.Range(6.6f, 14.1f);
        yield return new WaitForSeconds(rand);
        Destroy(this.gameObject);
    }
}
