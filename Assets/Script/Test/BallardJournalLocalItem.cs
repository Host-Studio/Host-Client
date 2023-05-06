using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallardJournalLocalItem : BallardJournalItem
{
    private string _localNametext;

    public void Init(BallardJournallPageType type, string localName)
    {
        _type = type;
        _localNametext = localName;
    }

}
