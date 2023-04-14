using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DialogueData
{
    int ID;
    string name;
    string context;
    int type;

    public DialogueData(int pID, string pname, string pcontext, int ptype)
    {
        ID = pID;
        name = pname;
        context = pcontext;
        type = ptype;
    }
}

public struct DialogueInfoData
{
    public struct Reward
    {
        int code;
        int Count;

        public Reward(int tCode, int tCount)
        {
            code = tCode;
            Count = tCount;
        }
    }

    int ID;
    int index;              // DialogueData.ID
    int type;               // 신원서 0, 요리 1, 장비 수리2
    int condition;
    int conditionCount;
    List<Reward> rewards;

    public DialogueInfoData(int tID, int tIndex, int tType, int tCodition, int tConditionCount, List<Reward> tList)
    {
        ID = tID;
        index = tIndex;
        type = tType;
        condition = tCodition;
        conditionCount = tConditionCount;
        rewards = tList;
    }
}

public class DialogueDB : MonoBehaviour
{
    /* 여기에 있는 데이터들은 추후에 DataManager에서 전체 관리 할 필요가 있음...
     * 정식님이 DataManager 만들어주면 거기로 이사 ㄲ */

    private List<DialogueData> DialogueDatas = new List<DialogueData>();            // Dialogue.csv
    private List<DialogueInfoData> DialogueInfos = new List<DialogueInfoData>();    // DialogueInfo.csv

    [SerializeField] private TextAsset dialogueCSV = null;
    [SerializeField] private TextAsset dialogueInfoCSV = null;

    private void Awake()
    {
        Parse();
    }

    private void Parse()
    {
        ParseDialogue();
        ParseDialogueInfo();
    }

    private void ParseDialogue()
    {
        string csvText = dialogueCSV.text.Substring(0, dialogueCSV.text.Length - 1);
        string[] originalRows = csvText.Split(new char[] { '\n' });
        string[] parsedRows = new string[100];

        string tID = "";
        string tName = "";
        string tDialogueContext = "";
        string tDialogueType = "";

        // 엑셀 파일 2번째 줄부터 시작
        for (int i = 2; i < originalRows.Length; i++)
        {
            int column = 0;
            char tChar;
            string tStr = "";
            bool end = true;

            // 한 줄 씩 파싱해서 parsedRows에 순서대로 저장
            for (int c = 0; c < originalRows[i].Length; c++)
            {
                tChar = originalRows[i][c];

                if (tChar == ',')
                {
                    // 한 column의 파싱이 끝남
                    if (end)
                    {
                        parsedRows[column++] = string.Copy(tStr);
                        tStr = "";
                        continue;
                    }
                }

                // 따옴표가 있으면 문장 내에 콤마가 있다는 의미이므로
                // 다음 따옴표가 등장하기 전까지 파싱을 계속한다.
                if (tChar == '\"')
                {
                    end = !end;
                    continue;
                }

                if(tChar == '\n' || tChar == '\r')
                    parsedRows[column++] = string.Copy(tStr);

                tStr += originalRows[i][c];
            }

            tID = parsedRows[0];
            tName = parsedRows[1];
            tDialogueContext = parsedRows[2];
            tDialogueType = parsedRows[3];

            if (tID == "") continue; // 공백

            DialogueDatas.Add(new DialogueData(int.Parse(tID), tName, tDialogueContext, int.Parse(tDialogueType)));
        }
    }

    private void ParseDialogueInfo()
    {
        string csvText = dialogueInfoCSV.text.Substring(0, dialogueInfoCSV.text.Length - 1);
        string[] originalRows = csvText.Split(new char[] { '\n' });
        string[] parsedRows = new string[100];

        string tID = "";
        string tIndex = "";
        string tType = "";
        string tCondition = "";
        string tConditionCount = "";
        string tCode = "";
        string tCount = "";

        // 엑셀 파일 2번째 줄부터 시작
        for (int i = 2; i < originalRows.Length; i++)
        {
            List<DialogueInfoData.Reward> rewards = new List<DialogueInfoData.Reward>();

            int column = 0;
            char tChar;
            string tStr = "";
            bool end = true;

            // 한 줄 씩 파싱해서 parsedRows에 순서대로 저장
            for (int c = 0; c < originalRows[i].Length; c++)
            {
                tChar = originalRows[i][c];

                if (tChar == ',')
                {
                    // 한 column의 파싱이 끝남
                    if (end)
                    {
                        parsedRows[column++] = string.Copy(tStr);
                        tStr = "";
                        continue;
                    }
                }

                // 따옴표가 있으면 문장 내에 콤마가 있다는 의미이므로
                // 다음 따옴표가 등장하기 전까지 파싱을 계속한다.
                if (tChar == '\"')
                {
                    end = !end;
                    continue;
                }

                if (tChar == '\n' || tChar == '\r')
                    parsedRows[column++] = string.Copy(tStr);

                tStr += originalRows[i][c];
            }

            tID = parsedRows[0];
            tIndex = parsedRows[1];
            tType = parsedRows[2];
            tCondition = parsedRows[3];
            tConditionCount = parsedRows[4];

            for(int j=0; j<4; j++)
            {
                tCode = parsedRows[5+j*2];
                tCount = parsedRows[5+j*2+1];

                rewards.Add(new DialogueInfoData.Reward(int.Parse(tCode), int.Parse(tCount)));
            }

            if (tID == "") continue; // 공백

            DialogueInfos.Add(new DialogueInfoData(int.Parse(tID), int.Parse(tIndex), int.Parse(tType), int.Parse(tCondition), int.Parse(tConditionCount), rewards));
        }
    }
}