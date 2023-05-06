public class BallardJournalPartyItem : BallardJournalItem
{
    private string _partyName;

    public void Init(BallardJournallPageType type, string partyName)
    {
        _type = type;
        _partyName = partyName;
    }

}
