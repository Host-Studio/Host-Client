using System.Collections.Generic;

public class UserData
{
    public struct Reputation
    {
        public int Guild;           // ��� ����
        public int Tribe;           // ���� ����
        public int Empire;          // ���� ����
    }

    public struct ServiceResult
    {
        public int gold;            // ��� ��ȭ��
        public List<Item> items;    // ���� ������
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
