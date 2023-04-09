using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private bool permit;    // ���� ��������, ���� ��������
    [SerializeField]
    private GameObject sealPrefab;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    private static Vector2 defaultPos;  // ���� ���� ��ġ
    private GameObject seal;    // ������ ���� ������Ʈ
    private GameObject parent;  // ������ ���� ������Ʈ�� �θ� ������Ʈ(stamp area)

    // �ʿ��� ������Ʈ
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
            Debug.Log("������ ����");
            Identity identity = canvas.transform.Find("Identity(Clone)").GetComponent<Identity>();

            // ������ ���� ���� ���ٸ�
            if (!identity.Sealing())
            {
                // ���� ������Ʈ ����
                seal = Instantiate(sealPrefab, currentPos, Quaternion.identity);
                parent = hostManager.localIdentity.transform.Find("StampArea").gameObject;
                seal.transform.SetParent(parent.transform);

                identity.SetPermit(permit);   // �ſ��� ������Ʈ�� ���� ���� ����
            }
        }
        else
        {
            Debug.Log("null");
        }
        this.transform.position = defaultPos;   // ���ڸ��� ������
    }


}
