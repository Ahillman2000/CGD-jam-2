using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalculateKarma : MonoBehaviour
{

    [SerializeField]private List<GameObject> Buildings;

    [SerializeField] private Squid player;
    private float karmaValue;

    private void OnEnable()
    {
        EventManager.StartListening("EnemyKilled", UpdateKarma);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyKilled", UpdateKarma);
    }

    private void OnApplicationQuit()
    {
        Destroy(this);
        EventManager.StopListening("EnemyKilled", UpdateKarma);
    }

    public void setBuildingValue(float buildingValue)
    {
        karmaValue = buildingValue;
    }

    private void ModifyKarma()
    {
        player.SetKarma(player.GetKarma() + karmaValue);
        karmaValue = 0;
    }

    void Update()
    {
        foreach(GameObject build in Buildings.ToArray())
        {
            if(build == null)
            {
                ModifyKarma();
                Buildings.Remove(build);
            }
        }

        if(player.GetKarma() >= 3000)
        {
            //I know why we did this, but this is definitely just a temporary solution, it's too abrupt as is when you win and doesn't even explain anything
            /// agreed - charlie
            SceneManager.LoadScene("Goodend");
        }
    }
    private void UpdateKarma(EventParam enemyKarma) // Ik there already is a ModifyKarma function but will need to rework it a lil cuz of the events. This works for now.
    {
        player.SetKarma(player.GetKarma() + enemyKarma.int_);
        karmaValue = 0;
    }
}
