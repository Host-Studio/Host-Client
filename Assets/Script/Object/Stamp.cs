using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas         canvas;
    [SerializeField] private GameObject     pfSeal_Accept, pfSeal_Deny;

    private GraphicRaycaster                graphicRaycaster;

    private static Vector2                  originPos;     // ���� ���� ��ġ

    private GameObject                      obSeal;
    private bool                            bAccepted;

    void Start()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

        if (gameObject.name == "Deny")
        {
            obSeal = pfSeal_Deny;
            bAccepted = false;
        }

        else if (gameObject.name == "Accept")
        {
            obSeal = pfSeal_Accept;
            bAccepted = true;
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = this.transform.position;
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
        transform.position = currentPos;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach(var v in results)
        {
            print(v.gameObject.name);
        }

        if (results.Count > 1 && results.Exists(x => x.gameObject.name == "StampArea"))
        {
            Debug.Log("������ ����");

            // ���� X
            if (!ServiceMain.instance.isSealed)
            {
                GameObject seal = Instantiate(obSeal, currentPos, Quaternion.identity);
                GameObject parent = results.Find(x => x.gameObject.name == "StampArea").gameObject;
                seal.transform.SetParent(parent.transform);

                ServiceMain.instance.isSealed = true;
                ServiceMain.instance.isAccepted = bAccepted;   // �ſ��� ������Ʈ�� ���� ���� ����
            }
        }
        else
        {
            Debug.Log("null");
        }

        this.transform.position = originPos;   // ���ڸ��� ������
    }
}
