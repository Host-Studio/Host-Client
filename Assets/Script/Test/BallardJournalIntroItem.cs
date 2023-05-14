
using UnityEngine;

public class BallardJournalIntroItem : BallardJournalItem
{
    public string IntroName
    {
        get;
        private set;
    }
    public void Init(BallardJournallPageType type, string introName, Sprite icon)
    {
        Type = type;
        IntroName = introName;
        Icon = icon;
    }
}
