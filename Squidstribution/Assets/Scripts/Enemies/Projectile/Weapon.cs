using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletContainer;

    [SerializeField] private GameObject bulletPrefab;
    //public GameObject particleEffect;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 30.0f;
    [SerializeField] private float cooldown    = 2.0f;
    [SerializeField] private float lifeTime    = 3.0f;
    [SerializeField] private bool randomCooldown = false;
    private bool inRange = false;

    private float timePassed = 0.0f;

    private void Start()
    {
        timePassed = cooldown;
    }

    private void Update()
    {
        if(inRange)
        {
            if (randomCooldown)
            {
                cooldown = Random.Range(1, 4);
            }
            if (Time.time > timePassed)
            {
                Fire();
                timePassed = Time.time + cooldown;
            }
        }
    }

    private void Fire()
    {
        EventParam eventParam = new EventParam();
        eventParam.soundstr_ = "GunFire";
        EventManager.TriggerEvent("GunFire", eventParam);

        /// Create bullet and parent to the object fired from
        GameObject bullet = Instantiate(bulletPrefab/*, this.transform*/);
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.SetParent(bulletContainer);

        /*GameObject muzzleflash = Instantiate(particleEffect);
        particleEffect.transform.position = bulletSpawn.position;
        //Destroy(muzzleflash);*/

        /// Converts Quaternion rotation to Vector3 to read from 0 to 360 in angles
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        /// Apply force to the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        /// Ignore arm collision
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
