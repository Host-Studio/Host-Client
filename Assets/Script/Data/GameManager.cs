using System;
using System.Collections.Generic;
using UnityEngine;

class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }




    private int iGold;


    private int iDate;
    public int Date { get { return iDate; } }
    private List<int> liReputations;




    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}