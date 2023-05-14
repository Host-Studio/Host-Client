
using UnityEngine;

public class BallardJournalLocalItem : BallardJournalItem
{
    public string LocalName
    {
        get;
        private set;
    }

    public void Init(BallardJournallPageType type, string localName, Sprite icon)
    {
        Type = type;
        LocalName = localName;
        Icon = icon;
    }

}
