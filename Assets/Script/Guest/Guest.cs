using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest
{
    // 이름
    private string name;
    public string Name
    {
        get { return name; }
    }

    // 지역
    private string local;
    public string Local
    {
        get { return local; }
    }

    // 세력
    private string party;
    public string Party
    {
        get { return party; }
    }

    // 종족
    private GuestDB.SpeciesType species;
    public GuestDB.SpeciesType Species
    {
        get { return species; }
    }

    // 직업
    private GuestDB.ProfessionType profession;
    public GuestDB.ProfessionType Profession
    {
        get { return profession; }
    }

    // 직업 인장
    private Sprite professionSeal;
    public Sprite ProfessionSeal
    {
        get { return professionSeal; }
    }

    // 티어
    private int tier;
    public int Tier
    {
        get { return tier; }
    }

    // 티어 증표
    private Sprite tierSeal;
    public Sprite TierSeal
    {
        get { return tierSeal; }
    }

    // 무기
    private GuestDB.WeaponInfo weapon;
    public GuestDB.WeaponInfo Weapon { get; }

    // 생성자
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
