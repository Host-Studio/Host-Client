using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // private List<string> nameList = new List<string>() { "�߶��", "�̾���Ʈ", "��ũ", "��������", "��ī" };
    private List<string> nameList = new List<string>() { "�߶��", "�̾���Ʈ", "��ũ", "��ī", "������", "�繮��" };

    // ����-����
    [SerializeField]
    private List<Sprite> professionSealList;    // ���� �̹��� ����Ʈ
    private Dictionary<GuestDB.ProfessionType, Sprite> professionToSeal = new Dictionary<GuestDB.ProfessionType, Sprite>();    // ������ ���� ���� �̹���

    // Ƽ��-��ǥ
    [SerializeField]
    private List<Sprite> tierSealList;    // ��ǥ �̹��� ����Ʈ


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

        // �̸� ����
        name = nameList[Random.Range(0, nameList.Count)];

        // ���� ����
        species = GuestDB.GetRandomSpecies();

        // ������ ���� ���� ����
        local = GuestDB.GetRandomLocal(species);

        // ������ ���� �ùٸ� ���� ����
        party = GuestDB.GetRandomParty(local);

        // ������ ���� �ùٸ� ���� ����
        profession = GuestDB.GetRandomProfession(local);

        // ������ ���� �ùٸ� ���� ����
        professionSeal = professionToSeal[profession];

        // Ƽ�� ����
        tier = Random.Range(0, 3);

        // �̸��� ���� ���� ����
        weapon = GuestDB.GetWeaponInfo(name);

        // Ƽ� ���� �ùٸ� ��ǥ ����
        tierSeal = tierSealList[tier];

        // ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� or ������ ���� �ʴ� ���� ����
        if (!correct)
        {
            int wrongKind;

            // ������ �������� �ʴ� ������ ���ٸ� ������ ���� �ʴ� ���� ���� �Ұ���
            if (GuestDB.GetWrongRandomProfession(local) == (GuestDB.ProfessionType) (-1))
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 3);
            else
                wrongKind = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, 4);

            switch (wrongKind)
            {
                case 0: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    // ���� ������ �ٸ� ���� ���ϱ�
                    string wrongLocal;
                    do
                    {
                        wrongLocal = GuestDB.GetRandomLocal(species);
                    } while (wrongLocal == local);

                    // �ٸ� ������ ���� ���ϱ�
                    party = GuestDB.GetRandomParty(wrongLocal);

                    break;

                case 1: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    // ���� ������ �ٸ� ���� ���ϱ�
                    GuestDB.ProfessionType wrongProfession;
                    do
                    {
                        wrongProfession= GuestDB.GetRandomProfession(local);
                    } while (wrongProfession == profession);

                    // �ٸ� ������ �������� ����
                    professionSeal = professionToSeal[wrongProfession];

                    break;

                case 2: // Ƽ�� ��ǥ ����ġ
                    Debug.Log("Ƽ��=��ǥ");

                    // ���� Ƽ��� �ٸ� Ƽ�� ���ϱ�
                    int count = tierSealList.Count;
                    int wrongTier = Random.Range(0, count);
                    while (tier == wrongTier)
                        wrongTier = Random.Range(0, count);

                    // �ٸ� Ƽ���� ��ǥ�� ����
                    tierSeal = tierSealList[wrongTier];

                    break;

                case 3: // ���� ���� ����ġ
                    Debug.Log("����=����");

                    profession = GuestDB.GetWrongRandomProfession(local);    // �ش� ������ ���� ���� ����

                    break;

                default:
                    break;
            }
        }
        
        return new Guest(name, local, party, species, profession, professionSeal, ++tier, tierSeal, weapon);
    }
}
