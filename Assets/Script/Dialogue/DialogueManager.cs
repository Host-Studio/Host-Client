using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Quaternion myRotation;  // ���� ��ǳ�� ȸ����
    private List<GameObject> speechBubbleList = new List<GameObject>();     // ������ ��ǳ�� ������Ʈ�� ���� ����Ʈ (���� ������Ʈ ������ ����)

    [SerializeField] private float spawnTime;    // ��ǳ���� ��µǴ� ����

    // �ʿ��� ������Ʈ
    [SerializeField] private GameObject guestBubblePrefab, myBubblePrefab;   // ���谡/�� ��ǳ�� ������
    [SerializeField] private GameObject parent;  // ��ǳ���� �θ� ������Ʈ(Scroll View�� Content ������Ʈ)

    private void Start()
    {
        myRotation = Quaternion.Euler(new Vector3(0, 180, 0));  // ���� ��ǳ�� ȸ����
    }

    // code : 0->�ȳ�, 1->����, 2->����
    // code�� ���� ��ȭ ���
    public IEnumerator GuestDialogueCoroutine(GuestDB.ProfessionType _profession, int code)
    {
        if (code == 0)  DeleteSpeechBubble();   // ���ο� ���谡 �湮 -> ���� ���谡���� ��ȭ ����

        string profession = _profession.ToString();
        TalkData[] talkDatas = DialogueParse.GetDialogue(profession + code.ToString());  // ��ȭ ������ ��������

        foreach (var talkData in talkDatas)
        {
            GameObject speechBubble;    // ������ ��ǳ�� ������Ʈ

            foreach (var context in talkData.contexts)
            {
                // ��ȭ�ڿ� ���� �ٸ� ��ǳ�� ���
                if (talkData.name != "��") // ��ȭ�� : ���谡
                {
                    speechBubble = Instantiate(guestBubblePrefab, Vector2.zero, Quaternion.identity);
                    speechBubble.transform.SetParent(parent.transform);
                }
                else    // ��ȭ�� : ��
                {
                    speechBubble = Instantiate(myBubblePrefab, Vector2.zero, myRotation);
                    speechBubble.transform.SetParent(parent.transform);
                }

                speechBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = context; // �ؽ�Ʈ ����
                speechBubbleList.Add(speechBubble); // ��ǳ�� ������ ���� ����Ʈ�� �߰�
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    // ��ǳ�� ������Ʈ ����
    private void DeleteSpeechBubble()
    {
        foreach (var bubble in speechBubbleList)
            Destroy(bubble);
        speechBubbleList.Clear();
    }
}
