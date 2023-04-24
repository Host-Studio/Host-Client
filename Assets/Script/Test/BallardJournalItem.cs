using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallardJournalItem : MonoBehaviour
{
    ///////////////////////////////////////
    // public
    public int _page;


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    ///////////////////////////////////////
    // private
    [SerializeField] private BallardJournallItemType _type;
}
