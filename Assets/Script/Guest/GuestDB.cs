using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���谡 ������ �ҷ�����
public class GuestDB : MonoBehaviour
{
    public enum ProfessionType
    {
        Warrior,    // ����
        Assassin,   // �ϻ���
        Mage,       // ������
        Bard,       // ��������
        Priest,     // ����
        Astrologian,// ��������
        Hunter,     // ��ɲ�
    }

    public enum SpeciesType
    {
        Human,
        Dwarf,
        Elf,
    }

    public enum MoneyType
    {
        Gold,
        Crown,
        Sealing,
        Deek,
    }

    public class WeaponInfo
    {
        public Dictionary<string, int> stateDict = new Dictionary<string, int>();
        public struct WeaponState
        {
            public int iDurabilityState;
            public int iDamageState;
            public int iDefenseState;
            public bool bCurseState;
            public int iRuneLevel;
        }

        public string name;
        public WeaponState state;

        public WeaponInfo(string pstrname, int piDurabilityState, int piDamageState, int piDefenseState, bool pbCurseState, int piRuneLevel)
        {
            name = string.Copy(pstrname);
            state.iDurabilityState = piDurabilityState;
            state.iDamageState = piDamageState;
            state.iDefenseState = piDefenseState;
            state.bCurseState = pbCurseState;
            state.iRuneLevel = piRuneLevel;

            stateDict["������"] = piDurabilityState;
            stateDict["���ݷ�"] = piDamageState;
            stateDict["����"] = piDefenseState;
        }

        public WeaponInfo(WeaponInfo weapon)
        {
            name = string.Copy(weapon.name);
            state.iDurabilityState = weapon.state.iDurabilityState;
            state.iDamageState = weapon.state.iDamageState;
            state.iDefenseState = weapon.state.iDefenseState;
            state.bCurseState = weapon.state.bCurseState;
            state.iRuneLevel = weapon.state.iRuneLevel;

            stateDict["������"] = weapon.state.iDurabilityState;
            stateDict["���ݷ�"] = weapon.state.iDamageState;
            stateDict["����"] = weapon.state.iDefenseState;
        }

        public void Set(WeaponInfo weapon)
        {
            name = string.Copy(weapon.name);
            state.iDurabilityState = weapon.state.iDurabilityState;
            state.iDamageState = weapon.state.iDamageState;
            state.iDefenseState = weapon.state.iDefenseState;
            state.bCurseState = weapon.state.bCurseState;
            state.iRuneLevel = weapon.state.iRuneLevel;

            stateDict["������"] = weapon.state.iDurabilityState;
            stateDict["���ݷ�"] = weapon.state.iDamageState;
            stateDict["����"] = weapon.state.iDefenseState;
        }
    }

    private static Dictionary<SpeciesType, string[]> localDictionary = new Dictionary<SpeciesType, string[]>();   // ������ ���� ����
    private static Dictionary<string, string[]> partyDictionary = new Dictionary<string, string[]>();   // ������ ���� ����
    private static Dictionary<string, ProfessionType[]> professionDictionary = new Dictionary<string, ProfessionType[]>();   // ������ ���� ����
    private static Dictionary<string, ProfessionType[]> nonProfessionDictionary = new Dictionary<string, ProfessionType[]>();   // ������ ���� ���� ����
    private static Dictionary<string, List<string>> dtWeaponDictionary = new Dictionary<string, List<string>>(); // ���� ���� (Name, WeaponName)

    [SerializeField] private TextAsset csvFile1 = null;     // ����-����-���� ������
    [SerializeField] private TextAsset csvFile2 = null;     // ����-���� ������
    [SerializeField] private TextAsset csvWeapon = null;    // ���� ������

    private void Awake()
    {
        SetGuestData();
        SetProfessionData();
        SetWeaponData();
    }

    // ���� ���� ����
    public static SpeciesType GetRandomSpecies()
    {
        int count = System.Enum.GetValues(typeof(SpeciesType)).Length;
        SpeciesType species = (SpeciesType)Random.Range(0, count);

        return species;
    }

    // ������ ���� ���� ����Ʈ ����
    public static string[] GetLocalList(SpeciesType species)
    {
        return localDictionary[species];
    }

    // ������ ���� ���� ���� ����
    public static string GetRandomLocal(SpeciesType species)
    {
        string[] localList = GetLocalList(species);
        return localList[Random.Range(0, localList.Length)];
    }

    // ������ ���� ���� ����Ʈ ����
    public static string[] GetPartyList(string local)
    {
        return partyDictionary[local];
    }

    // ������ ���� ���� ���� ����
    public static string GetRandomParty(string local)
    {
        string[] partyList = GetPartyList(local);
        return partyList[Random.Range(0, partyList.Length)];
    }

    // �ش� ������ ���� ����Ʈ ����
    public static ProfessionType[] GetProfessionList(string local)
    {
        return professionDictionary[local];
    }

    // ������ ���� �ùٸ� ���� ���� ����
    public static ProfessionType GetRandomProfession(string local)
    {
        ProfessionType[] professionList = GetProfessionList(local);
        return professionList[Random.Range(0, professionList.Length)];
    }

    // ������ ���� Ʋ�� ���� ���� ����
    public static ProfessionType GetWrongRandomProfession(string local)
    {
        ProfessionType[] wrongProfessionList = nonProfessionDictionary[local];
        if (wrongProfessionList.Length == 0)
            return (ProfessionType) (-1);
        else
            return wrongProfessionList[Random.Range(0, wrongProfessionList.Length)];
    }

    // �̸��� �´� ���� ����
    public static WeaponInfo GetWeaponInfo(string pstrGuestName = null)
    {
        WeaponInfo weapon;

        string tstrWeaponName = "";

        int tiDurabilityState = Random.Range(0, 5);
        int tiDamageState = Random.Range(0, 5);
        int tiDefenseState = Random.Range(0, 5);
        bool tbCurseState = Random.Range(0, 2) == 1 ? true : false;
        int tiRuneLevel = Random.Range(1, 4);

        if (pstrGuestName != null)
        {
            tstrWeaponName = dtWeaponDictionary[pstrGuestName][0];
        }

        else
        {
            List<string> tstrWeaponNames = dtWeaponDictionary["NPC"];
            tstrWeaponName = tstrWeaponNames[Random.Range(0, tstrWeaponNames.Count)];
        }

        return weapon = new WeaponInfo(tstrWeaponName, tiDurabilityState, tiDamageState, tiDefenseState, tbCurseState, tiRuneLevel);
    }

    // csvFile2(GuestDataFile) �Ľ�
    public void SetGuestData()
    {
        // �Ʒ� �� �� ����
        string csvText = csvFile1.text.Substring(0, csvFile1.text.Length - 1);
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });

        // ���� ���� 1��° ���� ���Ǹ� ���� �з��̹Ƿ� i = 1���� ����
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C���� �ɰ��� �迭�� ����
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // ��ȿ�� �̺�Ʈ �̸��� ���ö����� �ݺ�
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            SpeciesType species = (SpeciesType) System.Enum.Parse(typeof(SpeciesType), rowValues[0]);
            string[] localDatas = GetLocalDatas(rows, ref i, rowValues);

            localDictionary.Add(species, localDatas);
        }
    }

    string[] GetLocalDatas(string[] rows, ref int i, string[] rowValues)
    {
        List<string> localList = new List<string>();

        while (rowValues[0].Trim() != "end") // localList �ϳ��� ����� �ݺ���
        {
            string local = rowValues[1];
            List<string> partyList = new List<string>();
            for (int j = 2; j < rowValues.Length; j++)
            {
                if (rowValues[j].Trim() == "") break;   // ��ĭ�� ���
                partyList.Add(rowValues[j]);
            }

            localList.Add(local);
            partyDictionary.Add(local, partyList.ToArray());

            if (++i < rows.Length) rowValues =
                         rows[i].Split(new char[] { ',' });
            else break;
        }

        return localList.ToArray();
    }

    // csvFile2(ProfessionDataFile) �Ľ�
    public void SetProfessionData()
    {
        // �Ʒ� �� �� ����
        string csvText = csvFile2.text.Substring(0, csvFile2.text.Length - 1);
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });

        // ���� ���� 2��° �ٺ��� ����
        for (int i = 2; i < rows.Length; i++)
        {
            // A, B, C���� �ɰ��� �迭�� ����
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0] == "") continue;
            SetProfessionDictionary(rowValues);
        }
    }

    // professionDictionary, nonProfessionDictionary ����
    private void SetProfessionDictionary(string[] rowValues)
    {
        List<ProfessionType> professionList = new List<ProfessionType>();
        List<ProfessionType> nonProfessionList = new List<ProfessionType>();

        string local = rowValues[0];
        for (int i = 1; i < rowValues.Length; i++)
        {
            ProfessionType profession = (ProfessionType) (i - 1);
            switch (char.Parse(rowValues[i].Trim()))
            {
                case 'o':
                case 'O':
                    professionList.Add(profession);
                    break;
                case 'x':
                case 'X':
                    nonProfessionList.Add(profession);
                    break;
                default:
                    break;
            }
        }
        professionDictionary.Add(local, professionList.ToArray());
        nonProfessionDictionary.Add(local, nonProfessionList.ToArray());
    }

    public void SetWeaponData()
    {
        /* �߰��� ũ�� �ǳʶٴ� �κ��� �����ϱ� Ȯ���ؼ� �Ѱ���� �� */

        // ������ ���� ����
        string strCsvWeapon = csvWeapon.text.Substring(0, csvWeapon.text.Length - 1);

        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = strCsvWeapon.Split(new char[] { '\n' });

        // ���� ���� 2��° �ٺ��� ����
        for (int i = 2; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });
            
            string tId = rowValues[0];
            string tWeaponName = rowValues[1];
            string tOwnerName = rowValues[2];

            if (tId == "") break;
            
            if(!dtWeaponDictionary.ContainsKey(tOwnerName))
            {
                dtWeaponDictionary.Add(tOwnerName, new List<string>());
            }

            dtWeaponDictionary[tOwnerName].Add(tWeaponName);
        }
    }
}
