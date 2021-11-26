using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    float capturePercentageFloat = 0;
    int capturePercentageInt = 0;
    [SerializeField] float captureRate = 2;
    bool captured = false;

    [SerializeField] GameObject squidPrefab;
    [SerializeField] int squidToSpawn = 5;
    [SerializeField] float spawnRadius = 10;

    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("triggerStay");

        if(other.CompareTag("Player"))
        {
            if(!captured)
            {
                capturePercentageFloat += Time.deltaTime * captureRate;
                capturePercentageInt = (int)capturePercentageFloat;

                Debug.Log("Player capturing zone, " + capturePercentageInt + "%");

                if (capturePercentageInt >= 100)
                {
                    Debug.Log("Zone Captured");
                    captured = true;
                    SpawnSquids(squidToSpawn, new Vector3(this.transform.position.x, 0, this.transform.position.z), spawnRadius);
                }
            }
        }

        if (other.CompareTag("Enemy"))
        {
            if(capturePercentageInt > 0)
            {
                capturePercentageFloat -= Time.deltaTime * captureRate;
                capturePercentageInt = (int)capturePercentageFloat;

                Debug.Log("Player capturing zone, " + capturePercentageInt + "%");

                if (capturePercentageInt <= 0)
                {
                    Debug.Log("Zone Captured by enemy");
                }
            }
        }
    }

    void SpawnSquids(int numSquidsToSpawn, Vector3 point, float radius)
    {
        for (int i = 0; i < numSquidsToSpawn; i++)
        {

            // Distance around the circle
            var radians = 2 * Mathf.PI / numSquidsToSpawn * i;

            // Get the vector direction
            var vertrical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertrical);

            // Get the spawn position
            var spawnPos = point + spawnDir * radius;

            // Spawn squid
            var squid = Instantiate(squidPrefab, spawnPos, Quaternion.identity) as GameObject;

            // Rotate the enemy to face towards player
            squid.transform.LookAt(point);

            // Adjust height
            squid.transform.Translate(new Vector3(0, squid.transform.localScale.y / 2, 0));
        }
    }

    void Update()
    {
        
    }
}
