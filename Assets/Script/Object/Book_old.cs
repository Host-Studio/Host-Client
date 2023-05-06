using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Book_old : MonoBehaviour, IPointerClickHandler
{
    private int curPage = 0;
    private GameObject leftObj = null, rightObj = null;

    private GraphicRaycaster graphicRaycaster;

    [SerializeField]
    private List<Sprite> backgrounds = new List<Sprite>();  // J : �� �������� ��� �̹���
    [SerializeField]
    private Image LeftBackground, RightBackground;  // J : ����/������ ���
    [SerializeField]
    private GameObject MoveLeft, MoveRight; // J : Ŭ�� �� ������ �ѱ��
    [SerializeField]
    private Canvas canvas;


    // Start is called before the first frame update
    void Awake()
    {
        // J : ��ư ������ ����
        MoveLeft.GetComponent<Button>().onClick.AddListener(delegate { TryTurnOver(false); });   // J : moveLeft�� Button ������Ʈ�� ������ ����
        MoveRight.GetComponent<Button>().onClick.AddListener(delegate { TryTurnOver(true); });   // J : moveRight�� Button ������Ʈ�� ������ ����

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>(); // J : MoveLeft/MoveRight Ŭ�� ������ ����

        SetPage();
    }

    // J : å �ѱ�� �õ�
    private void TryTurnOver(bool right)
    {
        int offset = right ? 2 : -2;
        int nextPage = curPage + offset;

        TurnOver(nextPage);
    }

    // J : ����/������ �ѱ�� or �ǳʶٱ� �̵�
    public void TurnOver(int nextPage)
    {
        Debug.Log("å �ѱ��");
        
        // J : å �ѱ�� ����
        if (nextPage >= 0 && nextPage < backgrounds.Count)
        {
            curPage = nextPage;
            SetPage();
        }
    }

    // J : ������ ��� �� ������ ����
    private void SetPage()
    {
        // J : ��� ����
        if (backgrounds[curPage] != null)
            LeftBackground.sprite = backgrounds[curPage];
        if (backgrounds[curPage + 1] != null)
            RightBackground.sprite = backgrounds[curPage + 1];

        // J : ���� ������ ����
        if (leftObj != null)
            Destroy(leftObj);
        if (rightObj != null)
            Destroy (rightObj);

        // J : ������ ����
        leftObj = Resources.Load<GameObject>("Book/Page" + curPage.ToString());
        rightObj = Resources.Load<GameObject>("Book/Page" + (curPage + 1).ToString());

        if (leftObj != null)
            leftObj = SpawnContents(leftObj, LeftBackground);
        if (rightObj != null)
            rightObj = SpawnContents(rightObj, RightBackground);
    }

    // J : ������ ������Ʈ ����, ������ ������ ������Ʈ ����
    private GameObject SpawnContents(GameObject obj, Image background)
    {
        GameObject createObj = Instantiate(obj, Vector2.zero, Quaternion.identity); // J : ������Ʈ ����
        createObj.transform.parent = background.transform;  // J : �θ� ������Ʈ ����

        // J : ��ġ �� ũ�� ����
        createObj.transform.localPosition = Vector2.zero;
        createObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        return createObj;
    }

    // J : ������ ������Ʈ�� �ִ� ��� MoveLeft/MoveRight�� �׻� PageX���� ���� ������
    // J : -> ��ư Ŭ���� ���� �����Ƿ� ����ĳ��Ʈ�� ���� Ŭ�� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // J : Ŭ���� ��ġ�� ����ĳ��Ʈ�� ������Ʈ ���
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);


        for (int i = 1; i < results.Count; i++)
            if (results[i].gameObject.name=="MoveLeft" || results[i].gameObject.name == "MoveRight")    // J : MoveLeft�� MoveRight�� Ŭ��
                results[i].gameObject.GetComponent<Button>().onClick.Invoke();  // J : �̸� ����� ������ ȣ��
    }
}
