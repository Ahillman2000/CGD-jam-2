using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateKarma : MonoBehaviour
{

    [SerializeField]private List<GameObject> goodBuildings;
    [SerializeField]private List<GameObject> badBuildings;

    [SerializeField] private Squid player;

    private void AddKarma()
    {
        player.SetKarma(player.GetKarma() + 25);
    }

    private void RemoveKarma()
    {
        player.SetKarma(player.GetKarma() - 25);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject build in goodBuildings)
        {
            if(build == null)
            {
                RemoveKarma();
                goodBuildings.Remove(build);
            }
        }
        
        
        foreach (GameObject build in badBuildings)
        {
            if (build == null)
            {
                AddKarma();
                badBuildings.Remove(build);
                
            }
        }
    }

}
