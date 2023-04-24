using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallardJournalLocalItem : BallardJournalItem
{
    [SerializeField] private Text _localNametext;

    public void Init(string localName)
    {
        _localNametext.text = localName;
    }

}
