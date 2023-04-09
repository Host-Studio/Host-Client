using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private string objectName;  // 오브젝트 이름(Identity or TierSeal)

    [SerializeField]
    [Range(0, 1)]
    private float deskAreaY;    // 전체 높이에서 책상 비율
    private float deskHeight;   // 책상 높이
    private float dragTime;     // 드래그한 시간
    private bool isCloseUp;     // 현재 확대 상태인지
    private static Vector2 defaultPos;  // 기존 위치

    // 필요한 컴포넌트
    private GameObject canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    void Start()
    {
        deskHeight = Screen.height * deskAreaY;

        canvas = GameObject.Find("Canvas");
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            this.transform.position = currentPos;
            dragTime += Time.deltaTime; // 드래그 시간 업데이트
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!isCloseUp)
        {
            Vector2 currentPos = eventData.position;
            this.transform.position = pointerEventData.position = currentPos;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            if (currentPos.y > deskHeight)    // 책상 외부인 경우
            {
                Identity identity = gameObject.GetComponent<Identity>();
                HostManager hostManager = GameObject.Find("HostManager").GetComponent<HostManager>();
                if (results.Count > 1 && results[1].gameObject.name == "Character" && identity != null && identity.sealing)  // 인장이 찍힌 신원서를 캐릭터에게 건내는 경우
                {
                    hostManager.SendIdentity(identity.permit);
                }
                else if (results.Count > 1 && results[1].gameObject.name == "Character" && identity == null)    // 도장을 캐릭터에게 건내는 경우
                {
                    hostManager.SendTierSeal();
                }
                else
                {
                    this.transform.position = defaultPos;   // 원위치로 이동
                }
            }
        }
    }

    public void Click()
    {
        if (dragTime == 0)  // 드래그 없이 클릭만 한 경우
        {
            isCloseUp = true;
            canvas.transform.Find("CloseUp").gameObject.SetActive(true);
            canvas.transform.Find("CloseUp").Find("CloseUp" + objectName).gameObject.SetActive(true);
        }
        dragTime = 0;
    }

    // 클로즈업 해제
    public void ReleaseCloseUp()
    {
        isCloseUp = false;
    }
}
