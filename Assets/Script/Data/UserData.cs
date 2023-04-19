
using System.Collections.Generic;

public class UserData
{
    public enum Reputation
    {
        Guild = 0,  // ��� ����
        Tribe,      // ���� ����
        Empire,     // ���� ����
    }

    public string uid;
    public int date;
    public int gold;
    public int[] reputations = new int[3];
    public int groupID;

    public UserData(string uid, int date, int gold, int guildRep, int TribeRep, int EmpireRep, int groupID)
    {
        this.uid = uid;
        this.date = date;
        this.gold = gold;
        reputations[(int)Reputation.Guild] = guildRep;
        reputations[(int)Reputation.Tribe] = TribeRep;
        reputations[(int)Reputation.Empire] = EmpireRep;
        this.groupID = groupID;
    }
}
