using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Quaternion myRotation;  // 나의 말풍선 회전값
    private List<GameObject> speechBubbleList = new List<GameObject>();     // 생성할 말풍선 오브젝트를 담을 리스트 (추후 오브젝트 삭제를 위함)

    [SerializeField] private float spawnTime;    // 말풍선이 출력되는 간격

    // 필요한 컴포넌트
    [SerializeField] private GameObject guestBubblePrefab, myBubblePrefab;   // 모험가/나 말풍선 프리팹
    [SerializeField] private GameObject parent;  // 말풍선의 부모 오브젝트(Scroll View의 Content 오브젝트)

    private void Start()
    {
        myRotation = Quaternion.Euler(new Vector3(0, 180, 0));  // 나의 말풍선 회전값
    }

    // code : 0->안내, 1->승인, 2->거절
    // code에 따른 대화 출력
    public IEnumerator GuestDialogueCoroutine(GuestDB.ProfessionType _profession, int code)
    {
        if (code == 0)  DeleteSpeechBubble();   // 새로운 모험가 방문 -> 이전 모험가와의 대화 삭제

        string profession = _profession.ToString();
        TalkData[] talkDatas = DialogueParse.GetDialogue(profession + code.ToString());  // 대화 데이터 가져오기

        foreach (var talkData in talkDatas)
        {
            GameObject speechBubble;    // 생성할 말풍선 오브젝트

            foreach (var context in talkData.contexts)
            {
                // 대화자에 따라 다른 말풍선 출력
                if (talkData.name != "나") // 대화자 : 모험가
                {
                    speechBubble = Instantiate(guestBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                }
                else    // 대화자 : 나
                {
                    speechBubble = Instantiate(myBubblePrefab, Vector2.zero, myRotation);
                    speechBubble.transform.SetParent(parent.transform);
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = context; // 텍스트 지정
                speechBubbleList.Add(speechBubble); // 말풍선 관리를 위해 리스트에 추가
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    // 말풍선 오브젝트 삭제
    private void DeleteSpeechBubble()
    {
        foreach (var bubble in speechBubbleList)
            Destroy(bubble);
        speechBubbleList.Clear();
    }
}
