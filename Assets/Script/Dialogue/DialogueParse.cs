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
        // 아래 한 줄 빼기
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        string[] rows = csvText.Split(new char[] { '\n' });
        
        // 엑셀 파일 1번째 줄은 편의를 위한 분류이므로 i = 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            // A, B, C열을 쪼개서 배열에 담음
            string[] rowValues = rows[i].Split(new char[] { ',' });

            // 유효한 이벤트 이름이 나올때까지 반복
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            string eventName = rowValues[0];
            TalkData[] talkDatas = GetTalkDatas(rows, ref i, rowValues);

            DialoueDictionary.Add(eventName, talkDatas);
        }
    }

    TalkData[] GetTalkDatas(string[] rows, ref int i, string[] rowValues)
    {
        List<TalkData> talkDataList = new List<TalkData>();

        while (rowValues[0].Trim() != "end") // talkDataList 하나를 만드는 반복문
        {
            // 캐릭터가 한번에 치는 대사의 길이를 모르므로 리스트로 선언
            List<string> contextList = new List<string>();

            TalkData talkData;
            talkData.name = rowValues[1]; // 캐릭터 이름이 있는 B열

            do // talkData 하나를 만드는 반복문
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