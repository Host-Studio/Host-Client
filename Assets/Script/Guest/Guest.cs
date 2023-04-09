using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest
{
    // �̸�
    private string name;
    public string Name
    {
        get { return name; }
    }

    // ����
    private string local;
    public string Local
    {
        get { return local; }
    }

    // ����
    private string party;
    public string Party
    {
        get { return party; }
    }

    // ����
    private GuestDB.SpeciesType species;
    public GuestDB.SpeciesType Species
    {
        get { return species; }
    }

    // ����
    private GuestDB.ProfessionType profession;
    public GuestDB.ProfessionType Profession
    {
        get { return profession; }
    }

    // ���� ����
    private Sprite professionSeal;
    public Sprite ProfessionSeal
    {
        get { return professionSeal; }
    }

    // Ƽ��
    private int tier;
    public int Tier
    {
        get { return tier; }
    }

    // Ƽ�� ��ǥ
    private Sprite tierSeal;
    public Sprite TierSeal
    {
        get { return tierSeal; }
    }

    // ����
    private GuestDB.WeaponInfo weapon;
    public GuestDB.WeaponInfo Weapon { get; }

    // ������
    public Guest(string _name, string _local, string _party, GuestDB.SpeciesType _species, GuestDB.ProfessionType _profession, Sprite _professionSeal, int _tier, Sprite _tierSeal, GuestDB.WeaponInfo _weapon)
    {
        name = _name;
        local = _local;
        party = _party;
        species = _species;
        profession = _profession;
        professionSeal = _professionSeal;
        tier = _tier;
        tierSeal = _tierSeal;
        weapon = new GuestDB.WeaponInfo(_weapon);
    }
}
