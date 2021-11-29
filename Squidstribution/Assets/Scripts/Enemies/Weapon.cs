using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30.0f;
    public float fireRate = 2.0f;
    public float lifeTime = 3.0f;
    private float timePassed;

    private void Start()
    {
        timePassed = fireRate;
    }

    private void Update()
    {
        if(Time.time > timePassed)
        {
            Fire();
            timePassed = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        
        // Ignore arm collision
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

        bullet.transform.position = bulletSpawn.position;

        // Converts Quaternion rotation to Vector3 to read from 0 to 360 in angles
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }
}
