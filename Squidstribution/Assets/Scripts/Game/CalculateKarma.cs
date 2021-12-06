using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalculateKarma : MonoBehaviour
{

    [SerializeField]private List<GameObject> goodBuildings;
    [SerializeField]private List<GameObject> badBuildings;

    [SerializeField] private Squid player;
    private float karmaValue;

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
        foreach(GameObject build in goodBuildings.ToArray())
        {
            if(build == null)
            {
                ModifyKarma();
                goodBuildings.Remove(build);
            }
        }
        
        
        foreach (GameObject build in badBuildings.ToArray())
        {
            if (build == null)
            {
                ModifyKarma();
                badBuildings.Remove(build);
            }
        }

        if(player.GetKarma() >= 1000)
        {
            SceneManager.LoadScene("Goodend");
        }
    }

}
