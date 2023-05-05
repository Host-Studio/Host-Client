using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovingPaperworkAndToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public GameObject           canvas;
    public Button               button;

    private Vector2             originPos;
    private GraphicRaycaster    graphicRaycaster;

    private float               dragTime = 0;

    public void Start()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;

        dragTime = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        transform.position = currentPos;

        dragTime += Time.deltaTime;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        transform.position = currentPos;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        // å�� ��
        if (!results.Exists(x => x.gameObject.name == "Desk"))
        {
            // ���谡���� ����
            if(ServiceMain.instance.isSealed && results.Exists(x => x.gameObject.name == "Character"))
            {
                // �ſ���
                if(results[0].gameObject.name == "Paperwork")
                {
                    ServiceMain.instance.SendPaperwork(ServiceMain.instance.isAccepted);
                }

                // ��ǥ
                else if (results[0].gameObject.name == "Token")
                {
                    ServiceMain.instance.SendToken();
                }
            }

            else
            {
                this.transform.position = originPos;   // ����ġ�� �̵�
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragTime <= 0f)
        {
            if (gameObject.name == "Paperwork")
                gameObject.GetComponent<CloseUp>().CloseUpSetAcitveTrue("�ſ���");
            else
                gameObject.GetComponent<CloseUp>().CloseUpSetAcitveTrue("��ǥ");
        }

        dragTime = 0;
    }
}