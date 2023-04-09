using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Book : MonoBehaviour, IPointerClickHandler
{
    private int curPage = 0;
    private GameObject leftObj = null, rightObj = null;

    private GraphicRaycaster graphicRaycaster;

    [SerializeField]
    private List<Sprite> backgrounds = new List<Sprite>();  // J : 각 페이지의 배경 이미지
    [SerializeField]
    private Image LeftBackground, RightBackground;  // J : 왼쪽/오른쪽 배경
    [SerializeField]
    private GameObject MoveLeft, MoveRight; // J : 클릭 시 페이지 넘기기
    [SerializeField]
    private Canvas canvas;


    // Start is called before the first frame update
    void Awake()
    {
        // J : 버튼 리스너 설정
        MoveLeft.GetComponent<Button>().onClick.AddListener(delegate { TryTurnOver(false); });   // J : moveLeft의 Button 컴포넌트에 리스너 설정
        MoveRight.GetComponent<Button>().onClick.AddListener(delegate { TryTurnOver(true); });   // J : moveRight의 Button 컴포넌트에 리스너 설정

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>(); // J : MoveLeft/MoveRight 클릭 감지를 위함

        SetPage();
    }

    // J : 책 넘기기 시도
    private void TryTurnOver(bool right)
    {
        int offset = right ? 2 : -2;
        int nextPage = curPage + offset;

        TurnOver(nextPage);
    }

    // J : 왼쪽/오른쪽 넘기기 or 건너뛰기 이동
    public void TurnOver(int nextPage)
    {
        Debug.Log("책 넘기기");
        
        // J : 책 넘기기 가능
        if (nextPage >= 0 && nextPage < backgrounds.Count)
        {
            curPage = nextPage;
            SetPage();
        }
    }

    // J : 페이지 배경 및 컨텐츠 설정
    private void SetPage()
    {
        // J : 배경 설정
        if (backgrounds[curPage] != null)
            LeftBackground.sprite = backgrounds[curPage];
        if (backgrounds[curPage + 1] != null)
            RightBackground.sprite = backgrounds[curPage + 1];

        // J : 기존 컨텐츠 삭제
        if (leftObj != null)
            Destroy(leftObj);
        if (rightObj != null)
            Destroy (rightObj);

        // J : 컨텐츠 설정
        leftObj = Resources.Load<GameObject>("Book/Page" + curPage.ToString());
        rightObj = Resources.Load<GameObject>("Book/Page" + (curPage + 1).ToString());

        if (leftObj != null)
            leftObj = SpawnContents(leftObj, LeftBackground);
        if (rightObj != null)
            rightObj = SpawnContents(rightObj, RightBackground);
    }

    // J : 컨텐츠 오브젝트 스폰, 생성한 컨텐츠 오브젝트 리턴
    private GameObject SpawnContents(GameObject obj, Image background)
    {
        GameObject createObj = Instantiate(obj, Vector2.zero, Quaternion.identity); // J : 오브젝트 스폰
        createObj.transform.parent = background.transform;  // J : 부모 오브젝트 지정

        // J : 위치 및 크기 지정
        createObj.transform.localPosition = Vector2.zero;
        createObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        return createObj;
    }

    // J : 컨텐츠 오브젝트가 있는 경우 MoveLeft/MoveRight가 항상 PageX보다 먼저 렌더링
    // J : -> 버튼 클릭이 되지 않으므로 레이캐스트를 통해 클릭 감지
    public void OnPointerClick(PointerEventData eventData)
    {
        // J : 클릭한 위치에 레이캐스트된 오브젝트 목록
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);


        for (int i = 1; i < results.Count; i++)
            if (results[i].gameObject.name=="MoveLeft" || results[i].gameObject.name == "MoveRight")    // J : MoveLeft나 MoveRight를 클릭
                results[i].gameObject.GetComponent<Button>().onClick.Invoke();  // J : 미리 등록한 리스너 호출
    }
}
