//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class SpawnObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
//{
//    [SerializeField]
//    private string objectName;  // ������Ʈ �̸�(Identity or TierSeal)

//    [SerializeField]
//    [Range(0, 1)]
//    private float deskAreaY;    // ��ü ���̿��� å�� ����
//    private float deskHeight;   // å�� ����
//    private float dragTime;     // �巡���� �ð�
//    private bool isCloseUp;     // ���� Ȯ�� ��������
//    private static Vector2 defaultPos;  // ���� ��ġ

//    // �ʿ��� ������Ʈ
//    private GameObject canvas;
//    private GraphicRaycaster graphicRaycaster;
//    private PointerEventData pointerEventData;

//    void Start()
//    {
//        deskHeight = Screen.height * deskAreaY;

//        canvas = GameObject.Find("Canvas");
//        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
//        pointerEventData = new PointerEventData(null);
//    }

//    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
//    {
//        defaultPos = this.transform.position;
//    }

//    void IDragHandler.OnDrag(PointerEventData eventData)
//    {
//        if (!isCloseUp)
//        {
//            Vector2 currentPos = eventData.position;
//            this.transform.position = currentPos;
//            dragTime += Time.deltaTime; // �巡�� �ð� ������Ʈ
//        }
//    }

//    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
//    {
//        if (!isCloseUp)
//        {
//            Vector2 currentPos = eventData.position;
//            this.transform.position = pointerEventData.position = currentPos;

//            List<RaycastResult> results = new List<RaycastResult>();
//            graphicRaycaster.Raycast(pointerEventData, results);

//            if (currentPos.y > deskHeight)    // å�� �ܺ��� ���
//            {
//                Identity identity = gameObject.GetComponent<Identity>();
//                //HostManager hostManager = GameObject.Find("HostManager").GetComponent<HostManager>();
//                ServiceMain service = GameObject.Find("HostManager").GetComponent<ServiceMain>();
//                if (results.Count > 1 && results[1].gameObject.name == "Character" && identity != null && identity.sealing)  // ������ ���� �ſ����� ĳ���Ϳ��� �ǳ��� ���
//                {
//                    service.SendPaperwork(identity.permit);
//                }
//                else if (results.Count > 1 && results[1].gameObject.name == "Character" && identity == null)    // ������ ĳ���Ϳ��� �ǳ��� ���
//                {
//                    service.SendToken();
//                }
//                else
//                {
//                    this.transform.position = defaultPos;   // ����ġ�� �̵�
//                }
//            }
//        }
//    }

//    public void Click()
//    {
//        if (dragTime == 0)  // �巡�� ���� Ŭ���� �� ���
//        {
//            isCloseUp = true;
//            canvas.transform.Find("CloseUp").gameObject.SetActive(true);
//            canvas.transform.Find("CloseUp").Find("CloseUp" + objectName).gameObject.SetActive(true);
//        }
//        dragTime = 0;
//    }

//    // Ŭ����� ����
//    public void ReleaseCloseUp()
//    {
//        isCloseUp = false;
//    }
//}
