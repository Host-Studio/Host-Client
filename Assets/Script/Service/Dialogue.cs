using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

class Dialogue : MonoBehaviour
{
    private void Start()
    {
        myRotation = Quaternion.Euler(new Vector3(0, 180, 0));

        _dialogueDBDatas = SpecDataManager.instance.DialogueDBDatas.FindAll(x => 1000 < x.group_id && x.group_id < 2000).ToList();
    }
    public IEnumerator GuestDialogueCoroutine(int group_id, int dialogueType = 0)
    {
        // 새로운 모험가 방문 -> 이전 모험가와의 대화 삭제
        if (dialogueType == 0) DeleteSpeechBubble();

        // 필요한 대화 그룹 로딩
        List<DialogueDBData> dialogueDatas = _dialogueDBDatas.FindAll(x => x.group_id == group_id && x.dialogue_type == dialogueType).ToList();

        foreach (DialogueDBData dialogueData in dialogueDatas)
        {
            GameObject speechBubble;    // 생성할 말풍선 오브젝트

            // 모험가
            if (dialogueData.npc_name != "나")
            {
                speechBubble = Instantiate(guestBubblePrefab, Vector2.zero, Quaternion.identity);
                speechBubble.transform.SetParent(parent.transform);
            }

            // 플레이어
            else
            {
                speechBubble = Instantiate(myBubblePrefab, Vector2.zero, myRotation);
                speechBubble.transform.SetParent(parent.transform);
            }

            speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = dialogueData.dialogue; // 텍스트 지정
            speechBubbleList.Add(speechBubble); // 말풍선 관리를 위해 리스트에 추가

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public int GetRandomGroupID()
    {
        int group_id;

        System.Random rand = new System.Random();
        int idx = rand.Next(_dialogueDBDatas.FindLastIndex(x => x.group_id >= 3000) - _dialogueDBDatas.FindIndex(x => x.group_id >= 3000));
        group_id = _dialogueDBDatas[idx + 3000].group_id;

        return group_id;
    }

    /////////////////// private
    private List<DialogueDBData> _dialogueDBDatas;                          // Dialogue DB

    private Quaternion myRotation;                                          // 나의 말풍선 회전값
    private List<GameObject> speechBubbleList = new List<GameObject>();     // 생성할 말풍선 오브젝트를 담을 리스트 (추후 오브젝트 삭제를 위함)

    [SerializeField] private float spawnTime;                               // 말풍선이 출력되는 간격

    [SerializeField] private GameObject guestBubblePrefab, myBubblePrefab;  // 말풍선 프리팹
    [SerializeField] private GameObject parent;                             // 말풍선의 부모 오브젝트(Scroll View의 Content 오브젝트)

    private void DeleteSpeechBubble()
    {
        foreach (var bubble in speechBubbleList)
            Destroy(bubble);
        speechBubbleList.Clear();
    }
}