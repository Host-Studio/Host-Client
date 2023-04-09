using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // private List<string> nameList = new List<string>() { "발라드", "이안하트", "디르크", "무에르테", "샤카" };
    private List<string> nameList = new List<string>() { "발라드", "이안하트", "디르크", "샤카", "마르코", "톨문드" };

    // 직업-인장
    [SerializeField]
    private List<Sprite> professionSealList;    // 인장 이미지 리스트
    private Dictionary<GuestDB.ProfessionType, Sprite> professionToSeal = new Dictionary<GuestDB.ProfessionType, Sprite>();    // 직업에 따른 인장 이미지

    // 티어-증표
    [SerializeField]
    private List<Sprite> tierSealList;    // 증표 이미지 리스트


    private void Start()
    {
        for (int i = 0; i < professionSealList.Count; i++)
            professionToSeal.Add((GuestDB.ProfessionType) i, professionSealList[i]);
    }

    public Guest CreateGuest(bool correct)
    {
        int tier;
        string name, local, party;
        GuestDB.SpeciesType species;
        GuestDB.ProfessionType profession;
        Sprite professionSeal, tierSeal;
        GuestDB.WeaponInfo weapon;

        // 이름 랜덤
        name = nameList[Random.Range(0, nameList.Count)];

        // 종족 랜덤
        species = GuestDB.GetRandomSpecies();

        // 종족에 따른 지역 설정
        local = GuestDB.GetRandomLocal(species);

        // 지역에 따른 올바른 세력 설정
        party = GuestDB.GetRandomParty(local);

        // 지역에 따른 올바른 직업 설정
        profession = GuestDB.GetRandomProfession(local);

        // 직업에 따른 올바른 인장 설정
        professionSeal = professionToSeal[profession];

        // 티어 랜덤
        tier = Random.Range(0, 3);

        // 이름에 따라 무기 설정
        weapon = GuestDB.GetWeaponInfo(name);

        // 티어에 따른 올바른 증표 설정
        tierSeal = tierSealList[tier];

        // 지역에 맞지 않는 세력 or 직업에 맞지 않는 인장 or 지역에 맞지 않는 직업 설정
        if (!correct)
        {
            int wrongKind;

            // 지역에 존재하지 않는 직업이 없다면 지역에 맞지 않는 직업 설정 불가능
            if (GuestDB.GetWrongRandomProfession(local) == (GuestDB.ProfessionType) (-1))
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 3);
            else
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 4);

            switch (wrongKind)
            {
                case 0: // 지역 세력 불일치
                    Debug.Log("지역=세력");

                    // 현재 지역과 다른 지역 정하기
                    string wrongLocal;
                    do
                    {
                        wrongLocal = GuestDB.GetRandomLocal(species);
                    } while (wrongLocal == local);

                    // 다른 지역의 세력 정하기
                    party = GuestDB.GetRandomParty(wrongLocal);

                    break;

                case 1: // 직업 인장 불일치
                    Debug.Log("직업=인장");

                    // 현재 직업과 다른 직업 정하기
                    GuestDB.ProfessionType wrongProfession;
                    do
                    {
                        wrongProfession= GuestDB.GetRandomProfession(local);
                    } while (wrongProfession == profession);

                    // 다른 직업의 인장으로 설정
                    professionSeal = professionToSeal[wrongProfession];

                    break;

                case 2: // 티어 증표 불일치
                    Debug.Log("티어=증표");

                    // 현재 티어와 다른 티어 정하기
                    int count = tierSealList.Count;
                    int wrongTier = Random.Range(0, count);
                    while (tier == wrongTier)
                        wrongTier = Random.Range(0, count);

                    // 다른 티어의 증표로 설정
                    tierSeal = tierSealList[wrongTier];

                    break;

                case 3: // 지역 직업 불일치
                    Debug.Log("지역=직업");

                    profession = GuestDB.GetWrongRandomProfession(local);    // 해당 지역에 없는 직업 설정

                    break;

                default:
                    break;
            }
        }
        
        return new Guest(name, local, party, species, profession, professionSeal, ++tier, tierSeal, weapon);
    }
}
