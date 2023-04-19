
using System.Collections.Generic;

public class UserData
{
    public enum Reputation
    {
        Guild = 0,  // 길드 평판
        Tribe,      // 부족 평판
        Empire,     // 제국 평판
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
