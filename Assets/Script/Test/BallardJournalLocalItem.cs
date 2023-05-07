
public class BallardJournalLocalItem : BallardJournalItem
{
    public string LocalName
    {
        get;
        private set;
    }

    public void Init(BallardJournallPageType type, string localName)
    {
        _type = type;
        LocalName = localName;
    }

}
