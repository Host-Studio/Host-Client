using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private bool permit;    // 승인 도장인지, 거절 도장인지
    [SerializeField]
    private GameObject sealPrefab;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private static Vector2 defaultPos;  // 도장 기존 위치
    private GameObject seal;    // 스폰한 인장 오브젝트
    private GameObject parent;  // 스폰한 인장 오브젝트의 부모 오브젝트(stamp area)

    // 필요한 컴포넌트
    [SerializeField]
    private HostManager hostManager;
    [SerializeField]
    private Canvas canvas;

    void Start()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position;
        this.transform.position = currentPos;
    }


    // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=silentjeong&logNo=221510880388
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 currentPos = eventData.position;
        this.transform.position = pointerEventData.position = currentPos;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        

        if (results.Count > 1 && results[1].gameObject.name == "StampArea")
        {
            Debug.Log("스탬프 영역");
            Identity identity = canvas.transform.Find("Identity(Clone)").GetComponent<Identity>();

            // 도장을 찍은 적이 없다면
            if (!identity.Sealing())
            {
                // 인장 오브젝트 스폰
                seal = Instantiate(sealPrefab, currentPos, Quaternion.identity);
                parent = hostManager.localIdentity.transform.Find("StampArea").gameObject;
                seal.transform.SetParent(parent.transform);

                identity.SetPermit(permit);   // 신원서 오브젝트에 승인 여부 세팅
            }
        }
        else
        {
            Debug.Log("null");
        }
        this.transform.position = defaultPos;   // 제자리로 돌리기
    }


}
