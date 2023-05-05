using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ServiceMain : SceneMain
{
    public GameObject canvas;
    public Animator   billAnimator;

    public GameObject obDetailPaperwork;
    public GameObject obDetailToken;

    public bool isAccepted { get; set; }
    public bool isSealed { get; set; }

    public static ServiceMain instance;

    private void Start()
    {
        if (App.instance == null)
        {
            SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
            {
                Init();
            });
            SpecDataManager.instance.Init(this);
        }

        instance = this;

        client = new Client();
        userData = new UserData("0", 0, 0, 0, 0, 0, 0);

        obCloseUp = canvas.transform.Find("CloseUp").gameObject;            // 클로즈업 오브젝트
        obPaperwork = canvas.transform.Find("Paperwork").gameObject;        // 신원서 오브젝트
        obToken = canvas.transform.Find("Token").gameObject;                // 증표 오브젝트

        originPaperworkPos = obPaperwork.transform.position;
        originTokenPos = obToken.transform.position;

        VisitGuest();
    }

    public override void Init(SceneParams param = null)
    {
        SpecDataManager.instance.DialogueDBDatas[0].id = 1;
    }

    /////////////////// private
    [SerializeField]
    private Vector2 paperworkPos, tokenPos;

    [SerializeField] 
    private Dialogue        dialogue;

    private Client          client;
    private UserData        userData;

    private VisitData       curVisitData;
    private PaperworkData   curPaperworkData;
    private RewardData      curRewardData;
    private AdventurerData  curAdventurerData;

    private int             iMainDialgueIdx = 1000;
    private int             iRandomDialogueIdx = 3000;

    private Vector2         originPaperworkPos;
    private Vector2         originTokenPos;

    private Guest           guest;                  // 생성한 모험가 정보
    private bool            bCorrect;               // 모험가 진위 여부
    private bool            bDecision;              // 승인/거절 여부

    private GuestManager    guestManager;

    [SerializeField]
    private GameObject      obCloseUp;              // 클로즈업 오브젝트
    [SerializeField]
    private GameObject      obPaperwork;            // 신원서 오브젝트
    [SerializeField]
    private GameObject      obToken;                // 증표 오브젝트

    public void VisitGuest()
    {
        curVisitData        = null;
        curRewardData       = null;
        curAdventurerData   = null;
        curPaperworkData    = null;



        // 모험가 데이터 Set
        curVisitData = client.GetVisitData(userData);



        // 신원서
        if (curVisitData.purpose_type == "신원서")
        {
            bCorrect = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < 70 ? true : false;

            curAdventurerData = client.GetAdventurerData(curVisitData);
            curPaperworkData = client.GetPaperworkData(curAdventurerData, bCorrect);
            curRewardData = client.GetRewardData(curVisitData);

            // 신분증 Set
            SetPaperworkAndToken(bCorrect);
        }

        // 요리
        else if (curVisitData.purpose_type == "요리")
        {

        }

        // 장비
        if (curVisitData.purpose_type == "장비")
        {

        }



        // 대화
        dialogue.StartCoroutine(dialogue.GuestDialogueCoroutine(curVisitData.group_id, curVisitData.dialogue_type));
    }

    private void SetPaperworkAndToken(bool bCorrect)
    {
        obPaperwork.SetActive(true);
        obToken.SetActive(true);

        obDetailPaperwork = obCloseUp.transform.Find("Paperwork").Find(curPaperworkData.local).gameObject;
        obDetailPaperwork.SetActive(true);

        obDetailToken = obCloseUp.transform.Find("CloseUpTierSeal").gameObject;

        // 데이터 채우기
        obDetailPaperwork.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = curPaperworkData.name;
        obDetailPaperwork.transform.Find("Party").GetComponent<TextMeshProUGUI>().text = curPaperworkData.party_name;
        obDetailPaperwork.transform.Find("Species").GetComponent<TextMeshProUGUI>().text = curPaperworkData.species;
        obDetailPaperwork.transform.Find("Tier").GetComponent<TextMeshProUGUI>().text = curPaperworkData.toekn_tier.ToString();
        obDetailPaperwork.transform.Find("Profession").Find("Text").GetComponent<TextMeshProUGUI>().text = curPaperworkData.class_name;

        // 이미지 표시
        //obPaperwork.transform.Find("Profession").Find("Image").GetComponent<Image>().sprite = curPaperworkData.;

        // 증표 이미지
        //detailToken.transform.GetComponent<Image>().sprite = guest.TierSeal;
    }

    public void SendPaperwork(bool _decision)
    {
        bDecision = _decision;                                      // 승인/거절 여부 저장
        
        obPaperwork.SetActive(false);                               // 축소 신원서 오브젝트 삭제
        obPaperwork.transform.localPosition = paperworkPos;         // 위치 초기화

        Destroy(obDetailPaperwork.transform.Find("StampArea").GetChild(0).gameObject);

        CheckComplete();
    }

    public void SendToken()
    {
        obToken.SetActive(false);                                   // 축소 토큰 오브젝트 삭제
        obToken.transform.localPosition = tokenPos;

        CheckComplete();
    }

    private void CheckComplete()
    {
        if (obPaperwork.activeInHierarchy == false && obToken.activeInHierarchy == false)
        {
            StartCoroutine(DecisionComplete());
        }
    }

    private IEnumerator DecisionComplete()
    {
        // 승인
        if (bDecision)
        {
            if (bCorrect)
            {
                HospitalityScore.Instance.correctAnswer++;
            }
            else
            {
                HospitalityScore.Instance.wrongAnswer++;
                billAnimator.SetTrigger("Print");   // 고지서 출력

            }
            yield return dialogue.StartCoroutine(dialogue.GuestDialogueCoroutine(curVisitData.group_id, 1));   // 승인 대화 출력
        }

        // 거절
        else
        {
            yield return dialogue.StartCoroutine(dialogue.GuestDialogueCoroutine(curVisitData.group_id, 2));   // 거절 대화 출력
        }

        // 결과 반영
        userData.groupID = curVisitData.group_id;
        
        switch(curVisitData.encounter_reputation)
        {
            case "제국": userData.reputations.Empire += curVisitData.reputation_count; break;
            case "길드": userData.reputations.Guild += curVisitData.reputation_count; break;
            case "부족": userData.reputations.Tribe += curVisitData.reputation_count; break;
        }

        isAccepted = false;
        isSealed = false;

        obPaperwork.transform.position = originPaperworkPos;
        obToken.transform.position = originTokenPos;

        yield return new WaitForSeconds(2f);

        VisitGuest();
    }
}
