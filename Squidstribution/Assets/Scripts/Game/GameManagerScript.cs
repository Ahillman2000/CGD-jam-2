using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] float threat = 0;
    [SerializeField] float karma = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetThreat(float _threat)
    {
        threat = _threat;
    }
    public float GetThreat()
    {
        return threat;
    }

    public void SetKarma(float _karma)
    {
        karma = _karma;
    }
    public float GetKarma()
    {
        return karma;
    }
}
