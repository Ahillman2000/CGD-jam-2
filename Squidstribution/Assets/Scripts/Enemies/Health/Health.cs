using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action onHealthDepleted;
    public event Action<float> onHealthChange;

    private int health;
    [SerializeField] private int maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            onHealthDepleted?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            health -= 50;
            float currentHealthPCT = (float)health / (float)maxHealth;
            onHealthChange?.Invoke(currentHealthPCT);
        }
    }
}
