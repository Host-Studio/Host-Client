using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallardJournalPartyItem : BallardJournalItem
{
    [SerializeField] private Text _partyNametext;

    public void Init(string partyName)
    {
        _partyNametext.text = partyName;
    }

}
