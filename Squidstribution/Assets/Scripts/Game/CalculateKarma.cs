using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalculateKarma : MonoBehaviour
{

    [SerializeField]private List<GameObject> Buildings;

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
        foreach(GameObject build in Buildings.ToArray())
        {
            if(build == null)
            {
                ModifyKarma();
                Buildings.Remove(build);
            }
        }

        //killing enemies maybe improves karma too? Or perhaps getting enemy/building related achievments do?
        if(player.GetKarma() >= 1000)
        {
            //I know why we did this, but this is definitely just a temporary solution, it's too abrupt as is when you win and doesn't even explain anything
            SceneManager.LoadScene("Goodend");
        }
    }

}
