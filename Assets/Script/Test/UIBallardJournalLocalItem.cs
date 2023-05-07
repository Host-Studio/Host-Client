using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallardJournalLocalItem : UIBallardJournalItem
{
    public Text _localName;


    public void Init(string localName)
    {
        _localName.text = localName;
    }
}
