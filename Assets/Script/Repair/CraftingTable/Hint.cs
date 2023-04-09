using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Repair
{
    class Hint : MonoBehaviour
    {
        public class HintData
        {
            public string ownerName;
            public string hintText;
        }

        [SerializeField] private TMP_Text HintText;
        [SerializeField] private TextAsset csvHint = null;

        private Dictionary<string, Dictionary<string, List<string>>> hintDict = new Dictionary<string, Dictionary<string, List<string>>>(); // 상황, 캐릭터 이름, 힌트 내용
        private List<KeyValuePair<string, int>> stateList = new List<KeyValuePair<string, int>>();

        void Awake()
        {
            SetHintDataFromCSV();
        }

        private void OnEnable()
        {
            SetHint();
        }

        public void SetHint()
        {
            if (RepairManager.Instance == null) return;

            foreach (KeyValuePair<string, int> pair in RepairManager.Instance.WeaponInfo.stateDict)
            {
                if (pair.Value < 2) // 매우 나쁨, 나쁨
                {
                    stateList.Add(pair);
                }
            }

            int idx = UnityEngine.Random.Range(0, stateList.Count);

            List<string> arrHint = GetHint(RepairManager.Instance.strOwnerName, stateList[idx].Key);

            if (arrHint.Count == 1) HintText.text = arrHint[0];
            else
            {
                // NPC의 경우 힌트 종류가 다양
            }

            stateList.Clear();
        }

        public List<string> GetHint(string pstrOwnerName, string pstrAction)
        {
            return hintDict[pstrAction][pstrOwnerName];
        }

        public void SetHintDataFromCSV()
        {
            string[] rowValues = new string[100];

            string tId = "";
            string tAction = "";
            string tHintText = "";
            string tOwnerName = "";

            // 마지막 공백 제외
            string strCsvWeapon = csvHint.text.Substring(0, csvHint.text.Length - 1);

            // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
            string[] rows = strCsvWeapon.Split(new char[] { '\n' });
            string[] nameList;

            // 엑셀 파일 2번째 줄부터 시작
            for (int i = 1; i < rows.Length - 1; i++)
            {
                // 문자열 파싱
                int column = 0;
                char tempChar;
                string tempStr = "";
                bool end = true;
                for(int c=0; c<rows[i].Length; c++)
                {
                    tempChar = rows[i][c];

                    // 다음 쉼표 발견할 때까지 붙여넣기
                    if (tempChar == ',')
                    {
                        if(end)
                        {
                            rowValues[column++] = string.Copy(tempStr);
                            tempStr = "";
                            continue;
                        }
                    }

                    // 따옴표 발견하면 다음 따옴표까지 붙여넣기
                    if (tempChar == '\"')
                    {
                        end = !end;
                        continue;
                    }
                    
                    tempStr += rows[i][c];
                }

                tId = rowValues[0];
                tHintText = rowValues[2];
                tOwnerName = rowValues[3];

                if (tId == "") continue;

                nameList = tOwnerName.Split(new char[] { ',', ' ' });

                // 새 액션 타입 시작
                if (rowValues[1] != "")
                    tAction = string.Copy(rowValues[1]);

                for (int n = 0; n < nameList.Length; n++)
                {
                    if (nameList[n] == "") continue;

                    // 이미 존재하는 액션
                    if (hintDict.ContainsKey(tAction))
                    {
                        // 이름이 없는 경우
                        if (!hintDict[tAction].ContainsKey(nameList[n]))
                            hintDict[tAction].Add(nameList[n], new List<string>() { tHintText });
                    }

                    else
                        hintDict.Add(tAction, new Dictionary<string, List<string>>() { { nameList[n], new List<string>() { tHintText } } });
                }
            }
        }
    }
}
