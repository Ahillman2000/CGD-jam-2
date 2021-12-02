using UnityEngine;

public class AimTowardsTarget : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        this.transform.LookAt(player);
        this.transform.rotation *= Quaternion.FromToRotation(Vector3.down, Vector3.forward);
    }
}
