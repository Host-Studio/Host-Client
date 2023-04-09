using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HostManager : MonoBehaviour
{
    public GameObject localIdentity;                                            // 현재 모험가 지역의 신분증

    private Guest guest;                                                        // 생성한 모험가 정보
    private bool correct;                                                       // 모험가 진위 여부
    private bool decision;                                                      // 승인/거절 여부
    private GameObject identity, tierSeal;                                      // 스폰한 신원서, 증표 오브젝트
    private GameObject stampArea;                                               // 인장 삭제를 위한 부모 오브젝트 (스탬프 영역)

    [SerializeField ]private float trueRatio;                                   // 신원서가 올바를 확률
    [SerializeField] private Vector2 spawnIdentityPos, spawnTierSealPos;        // 신원서, 증표 오브젝트의 스폰 위치
    [SerializeField] private int sibilinIndex;                                  // 신원서, 증표 오브젝트의 렌더링 순서

    // 필요한 컴포넌트
    [SerializeField] private GuestManager guestManager;
    [SerializeField] private DialogueManager dialogueManager;                   // 모험가 안내/승인/거절 대화 출력을 위함
    [SerializeField] private GameObject closeUpIdentity;                        // 클로즈업 오브젝트 (신원서)
    [SerializeField] private GameObject parent;                                 // 신원서, 증표 오브젝트의 부모 오브젝트(캔버스)
    [SerializeField] private GameObject identityPrefab, tierSealPrefab;         // 신원서 프리팹, 증표 프리팹
    [SerializeField] private Image guestTierSeal;                               // 티어 증표 이미지
    [SerializeField] private Animator billAnimator;                             // 고지서 출력 애니메이터


    // Start is called before the first frame update
    void Start()
    {
        UpdateDate();   // 날짜 증가
        VisitGuest();
    }

    public void VisitGuest()
    {
        // 신원서 오브젝트 스폰
        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
        identity.transform.SetParent(parent.transform);
        identity.transform.localPosition = spawnIdentityPos;
        identity.transform.SetSiblingIndex(sibilinIndex); // sibilinIndex번째로 렌더링

        // 증표 오브젝트 스폰
        tierSeal = Instantiate(tierSealPrefab, Vector2.zero, Quaternion.identity);
        tierSeal.transform.SetParent(parent.transform);
        tierSeal.transform.localPosition = spawnTierSealPos;
        tierSeal.transform.SetSiblingIndex(sibilinIndex); // sibilinIndex번째로 렌더링

        // 신원서 데이터 생성
        correct = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < trueRatio ? true : false;
        guest = guestManager.CreateGuest(correct);

        Setting();

        // 안내 대화 출력
        dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 0));
    }

    // 신원서(클로즈업) 및 증표 세팅
    private void Setting()
    {
        List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();   // 이름, 세력, 종족, 티어, 직업 텍스트

        // 모험가 지역의 신분증 활성화
        localIdentity = closeUpIdentity.transform.Find(guest.Local).gameObject;
        localIdentity.SetActive(true);

        // 텍스트 가져오기
        // 이름, 세력, 종족, 티어 텍스트
        for (int i = 0; i < 4; i++)
            textList.Add(localIdentity.transform.GetChild(i).GetComponent<TextMeshProUGUI>());

        // 직업 텍스트
        textList.Add(localIdentity.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>());

        // 스탬프 영역 가져오기 (추후 인장 삭제를 위해)
        stampArea = localIdentity.transform.Find("StampArea").gameObject;

        // 신원서(클로즈업)에 표시
        textList[0].text = guest.Name;
        textList[1].text = guest.Party;
        textList[2].text = guest.Species.ToString();
        textList[3].text = guest.Tier.ToString();
        textList[4].text = guest.Profession.ToString();

        // 직업 인장 이미지 표시
        Image guestProfessionSeal = localIdentity.transform.GetChild(4).GetChild(1).GetComponent<Image>();
        guestProfessionSeal.sprite = guest.ProfessionSeal;

        // 증표 이미지 표시
        guestTierSeal.sprite = guest.TierSeal;
    }

    public void UpdateDate()
    {
        DataController.Instance.gameData.UpdateDate();    // 날짜 업데이트
    }

    public void SendIdentity(bool _decision)
    {
        decision = _decision;   // 승인/거절 여부 저장
        Destroy(identity);      // 축소 신원서 오브젝트 삭제
        identity = null;        // 명시적으로 null 대입
        Destroy(stampArea.transform.GetChild(0).gameObject);   // 인장 오브젝트 삭제

        CheckComplete();
    }

    public void SendTierSeal()
    {
        Destroy(tierSeal);      // 축소 신원서 오브젝트 삭제
        tierSeal = null;        // 명시적으로 null 대입

        CheckComplete();
    }

    // 신원서, 증표 모두 전달했는지 확인
    private void CheckComplete()
    {
        if (identity == null && tierSeal == null)     // 신원서, 증표 오브젝트 모두 없음
        {
            StartCoroutine(DecisionComplete());
            localIdentity.SetActive(false); // 클로즈업 지역 신원서 오브젝트 비활성화
        }
    }

    private IEnumerator DecisionComplete()
    {
        if (decision)   // 승인한 경우
        {
            if (correct)
            {
                HospitalityScore.Instance.correctAnswer++;
            }
            else
            {
                HospitalityScore.Instance.wrongAnswer++;
                billAnimator.SetTrigger("Print");   // 고지서 출력

            }
            yield return dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 1));   // 승인 대화 출력
        }
        else    // 거절한 경우
        {
            yield return dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 2));   // 거절 대화 출력
        }

        // dialogManager의 코루틴이 종료되면 호출
        VisitGuest();
    }
}
