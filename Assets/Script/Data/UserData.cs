using System.Collections.Generic;

public class UserData
{
    public struct Reputation
    {
        public int Guild;           // 길드 평판
        public int Tribe;           // 부족 평판
        public int Empire;          // 제국 평판
    }

    public struct ServiceResult
    {
        public int gold;            // 골드 변화량
        public List<Item> items;    // 얻은 아이템
    }


    public string uid;
    public int date;
    public int gold;
    public Reputation reputations;
    public int groupID;

    public UserData(string uid, int date, int gold, int guildRep, int TribeRep, int EmpireRep, int groupID)
    {
        this.uid = uid;
        this.date = date;
        this.gold = gold;
        reputations.Guild = guildRep;
        reputations.Tribe = TribeRep;
        reputations.Empire = EmpireRep;
        this.groupID = groupID;
    }

    public void Set(string uid, int date, int gold, int guildRep, int TribeRep, int EmpireRep, int groupID)
    {
        this.uid = uid;
        this.date = date;
        this.gold = gold;
        reputations.Guild = guildRep;
        reputations.Tribe = TribeRep;
        reputations.Empire = EmpireRep;
        this.groupID = groupID;
    }
}
