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
        float rand = Random.Range(3.5f, 8.4f);
        yield return new WaitForSeconds(rand);
        Destroy(this.gameObject);
    }
}
