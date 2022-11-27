using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalculateKarma : MonoBehaviour
{
    public static CalculateKarma instance;

    float winTimer = 0f;

    int TargetKarma = 6500;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private Squid player;

    private void OnEnable()
    {
        EventManager.StartListening("EnemyKilled", UpdateKarma);
        EventManager.StartListening("BuildingDestroyed", UpdateKarma);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyKilled", UpdateKarma);
        EventManager.StopListening("BuildingDestroyed", UpdateKarma);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("EnemyKilled", UpdateKarma);
        EventManager.StopListening("BuildingDestroyed", UpdateKarma);
    }

    public float GetKarma()
    {
        return player.GetKarma();
    }

    void Update()
    {
        if (player.GetKarma() >= TargetKarma && player.gameObject.GetComponent<PlayerStats>().GetBossState() == true)
        {
            winTimer += Time.deltaTime;
            //I know why we did this, but this is definitely just a temporary solution, it's too abrupt as is when you win and doesn't even explain anything
            /// agreed - charlie
            /// 
            if (winTimer >= .5f)
            {
                SceneManager.LoadScene("Goodend");
            }
        }
    }
    private void UpdateKarma(EventParam enemyKarma) // Ik there already is a ModifyKarma function but will need to rework it a lil cuz of the events. This works for now.
    {
        player.SetKarma(player.GetKarma() + enemyKarma.int_);
        EventParam eventParam = new EventParam(); eventParam.float_ = player.GetKarma();
        EventManager.TriggerEvent("KarmaChange", eventParam);
    }
}
