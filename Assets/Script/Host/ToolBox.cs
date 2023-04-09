using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{

    private Queue<GameObject> toolQueue = new Queue<GameObject>();
    private bool isDoorOpen;

    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    private Transform tools;
    [SerializeField]
    private Animator doorAnimator, wheelAnimator, smokeAnimator;


    // Start is called before the first frame update
    void Start()
    {
        // J : ���� ���� �� ��� ���� ��ť
        foreach (Transform tool in tools)
            toolQueue.Enqueue(tool.gameObject);

        toolQueue.Peek().SetActive(true);   // J : ù��° ������Ʈ Ȱ��ȭ
    }

    // J : �������� ������ Ŭ��
    public void ClickDoorHandle()
    {
        // J : ���� �Ѳ� ���ݱ�
        doorAnimator.SetTrigger("Change");

        isDoorOpen = !isDoorOpen;   // J : �÷��� ����
        if (isDoorOpen) // J : ���� ���
            StartCoroutine(SmokeCoroutine());   // J : ���� �߻�
    }

    // J : ���� ���� ��ư Ŭ��
    public void ClickArrowBtn()
    {
        // J : ���� ������Ʈ ��Ȱ��ȭ
        GameObject curObj = toolQueue.Dequeue();
        curObj.SetActive(false);
        toolQueue.Enqueue(curObj);

        // J : ���� ������Ʈ Ȱ��ȭ
        curObj = toolQueue.Peek();
        curObj.SetActive(true);

        // J : ���� ȸ��
        wheelAnimator.SetTrigger("Rotate");
    }

    // J : ���� �ִϸ��̼� �ڷ�ƾ
    private IEnumerator SmokeCoroutine()
    {
        // J : ���� �߻�
        smoke.SetActive(true);
        smokeAnimator.SetTrigger("Smoke");

        // J : �ִϸ��̼� ������� ���
        while (smokeAnimator.IsInTransition(0) == false)
            yield return new WaitForEndOfFrame();

        // J : Smoke ������Ʈ ��Ȱ��ȭ
        smoke.SetActive(false);
    }
}
