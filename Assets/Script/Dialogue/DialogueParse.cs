using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://velog.io/@gkswh4860/Unity-%EC%97%91%EC%85%80-%EB%8C%80%ED%99%94-%EB%82%B4%EC%9A%A9%EC%9D%84-%EB%8C%80%ED%99%94-%EC%9D%B4%EB%A6%84%EC%9C%BC%EB%A1%9C-%EB%AC%B6%EC%96%B4%EC%84%9C-%EA%B0%80%EC%A0%B8%EC%98%A4%EA%B8%B0
public class DialogueParse : MonoBehaviour
{
    private static Dictionary<string, TalkData[]> DialoueDictionary = new Dictionary<string, TalkData[]>();

    [SerializeField] private TextAsset csvFile = null;

    private void Awake()
    {
        SetTalkDictionary();
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        return DialoueDictionary[eventName];
    }

    public void SetTalkDictionary()
    {
        // �Ʒ� �� �� ����
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // �ٹٲ�(�� ��)�� �������� csv ������ �ɰ��� string�迭�� �� ������� ����
        string[] rows = csvText.Split(new char[] { '\n' });
        
        // ���� ���� 1��° ���� ���Ǹ� ���� �з��̹Ƿ� i = 1���� ����
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C���� �ɰ��� �迭�� ����
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // ��ȿ�� �̺�Ʈ �̸��� ���ö����� �ݺ�
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            string eventName = rowValues[0];
            TalkData[] talkDatas = GetTalkDatas(rows, ref i, rowValues);

            DialoueDictionary.Add(eventName, talkDatas);
        }
    }

    TalkData[] GetTalkDatas(string[] rows, ref int i, string[] rowValues)
    {
        List<TalkData> talkDataList = new List<TalkData>();

        while (rowValues[0].Trim() != "end") // talkDataList �ϳ��� ����� �ݺ���
        {
            // ĳ���Ͱ� �ѹ��� ġ�� ����� ���̸� �𸣹Ƿ� ����Ʈ�� ����
            List<string> contextList = new List<string>();

            TalkData talkData;
            talkData.name = rowValues[1]; // ĳ���� �̸��� �ִ� B��

            do // talkData �ϳ��� ����� �ݺ���
            {
                contextList.Add(rowValues[2].ToString());
                if (++i < rows.Length) rowValues =
                         rows[i].Split(new char[] { ',' });
                else break;
            } while (rowValues[1] == "" && rowValues[0] != "end");

            talkData.contexts = contextList.ToArray();
            talkDataList.Add(talkData);
        }

        return talkDataList.ToArray();
    }
}