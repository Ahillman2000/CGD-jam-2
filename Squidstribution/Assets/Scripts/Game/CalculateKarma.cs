using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
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
    }

}
