using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // ����ü�� �ν����� â�� ���̰� �ϱ� ���� �۾�
public struct TalkData
{
    public string name; // ��� ġ�� ĳ���� �̸�
    public string[] contexts; // ��� ����
}

public class Dialogue : MonoBehaviour
{
    // ��ȭ �̺�Ʈ �̸�
    [SerializeField] string eventName;
    // ������ ������ TalkData �迭 
    [SerializeField] TalkData[] talkDatas;
}