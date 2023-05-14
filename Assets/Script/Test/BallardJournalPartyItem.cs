using UnityEngine;

public class BallardJournalPartyItem : BallardJournalItem
{
    public string PartyName
    {
        get;
        private set;
    }

    public void Init(BallardJournallPageType type, string partyName, Sprite icon)
    {
        Type = type;
        PartyName = partyName;
        Icon = icon;
    }

}
