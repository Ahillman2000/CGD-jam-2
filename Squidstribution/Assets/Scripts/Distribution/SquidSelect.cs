using System.Collections.Generic;
using UnityEngine;

public class SquidSelect : MonoBehaviour
{
    public List<GameObject> SquidList = new List<GameObject>();
    public List<GameObject> SquidsSelected = new List<GameObject>();

    public static SquidSelect Instance { get { return _instance; } }
    private static SquidSelect _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject selected_squid)
    {
        DeselectAll();
        SquidsSelected.Add(selected_squid);
        selected_squid.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ShiftSelect(GameObject selected_squid)
    {
        if(!SquidsSelected.Contains(selected_squid))
        {
            SquidsSelected.Add(selected_squid);
            selected_squid.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            selected_squid.transform.GetChild(0).gameObject.SetActive(false);
            SquidsSelected.Remove(selected_squid);
        }
    }

    public void DeselectAll()
    {
        foreach(GameObject squid in SquidsSelected)
        {
            squid.transform.GetChild(0).gameObject.SetActive(false);
        }
        SquidsSelected.Clear();
    }
}
