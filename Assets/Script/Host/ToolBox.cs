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
        // J : 도구 상자 내 모든 도구 인큐
        foreach (Transform tool in tools)
            toolQueue.Enqueue(tool.gameObject);

        toolQueue.Peek().SetActive(true);   // J : 첫번째 오브젝트 활성화
    }

    // J : 도구상자 손잡이 클릭
    public void ClickDoorHandle()
    {
        // J : 상자 뚜껑 여닫기
        doorAnimator.SetTrigger("Change");

        isDoorOpen = !isDoorOpen;   // J : 플래그 변경
        if (isDoorOpen) // J : 여는 경우
            StartCoroutine(SmokeCoroutine());   // J : 연기 발생
    }

    // J : 도구 변경 버튼 클릭
    public void ClickArrowBtn()
    {
        // J : 현재 오브젝트 비활성화
        GameObject curObj = toolQueue.Dequeue();
        curObj.SetActive(false);
        toolQueue.Enqueue(curObj);

        // J : 다음 오브젝트 활성화
        curObj = toolQueue.Peek();
        curObj.SetActive(true);

        // J : 원판 회전
        wheelAnimator.SetTrigger("Rotate");
    }

    // J : 연기 애니메이션 코루틴
    private IEnumerator SmokeCoroutine()
    {
        // J : 연기 발생
        smoke.SetActive(true);
        smokeAnimator.SetTrigger("Smoke");

        // J : 애니메이션 종료까지 대기
        while (smokeAnimator.IsInTransition(0) == false)
            yield return new WaitForEndOfFrame();

        // J : Smoke 오브젝트 비활성화
        smoke.SetActive(false);
    }
}
