public class BallardJournalPartyItem : BallardJournalItem
{
    public string PartyName
    {
        get;
        private set;
    }

    public void Init(BallardJournallPageType type, string partyName)
    {
        _type = type;
        PartyName = partyName;
    }

}
