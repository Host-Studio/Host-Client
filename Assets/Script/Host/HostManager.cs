//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class HostManager : MonoBehaviour
//{
//    public GameObject localIdentity;                                            // ���� ���谡 ������ �ź���

//    private Guest guest;                                                        // ������ ���谡 ����
//    private bool correct;                                                       // ���谡 ���� ����
//    private bool decision;                                                      // ����/���� ����
//    private GameObject identity, tierSeal;                                      // ������ �ſ���, ��ǥ ������Ʈ
//    private GameObject stampArea;                                               // ���� ������ ���� �θ� ������Ʈ (������ ����)

//    [SerializeField ]private float trueRatio;                                   // �ſ����� �ùٸ� Ȯ��
//    [SerializeField] private Vector2 spawnIdentityPos, spawnTierSealPos;        // �ſ���, ��ǥ ������Ʈ�� ���� ��ġ
//    [SerializeField] private int sibilinIndex;                                  // �ſ���, ��ǥ ������Ʈ�� ������ ����

//    // �ʿ��� ������Ʈ
//    [SerializeField] private GuestManager guestManager;
//    [SerializeField] private DialogueManager dialogueManager;                   // ���谡 �ȳ�/����/���� ��ȭ ����� ����
//    [SerializeField] private GameObject closeUpIdentity;                        // Ŭ����� ������Ʈ (�ſ���)
//    [SerializeField] private GameObject parent;                                 // �ſ���, ��ǥ ������Ʈ�� �θ� ������Ʈ(ĵ����)
//    [SerializeField] private GameObject identityPrefab, tierSealPrefab;         // �ſ��� ������, ��ǥ ������
//    [SerializeField] private Image guestTierSeal;                               // Ƽ�� ��ǥ �̹���
//    [SerializeField] private Animator billAnimator;                             // ������ ��� �ִϸ�����


//    // Start is called before the first frame update
//    void Start()
//    {
//        UpdateDate();   // ��¥ ����
//        VisitGuest();
//    }

//    public void VisitGuest()
//    {
//        // �ſ��� ������Ʈ ����
//        identity = Instantiate(identityPrefab, Vector2.zero, Quaternion.identity);
//        identity.transform.SetParent(parent.transform);
//        identity.transform.localPosition = spawnIdentityPos;
//        identity.transform.SetSiblingIndex(sibilinIndex); // sibilinIndex��°�� ������

//        // ��ǥ ������Ʈ ����
//        tierSeal = Instantiate(tierSealPrefab, Vector2.zero, Quaternion.identity);
//        tierSeal.transform.SetParent(parent.transform);
//        tierSeal.transform.localPosition = spawnTierSealPos;
//        tierSeal.transform.SetSiblingIndex(sibilinIndex); // sibilinIndex��°�� ������

//        // �ſ��� ������ ����
//        correct = new System.Random(System.Guid.NewGuid().GetHashCode()).NextDouble() < trueRatio ? true : false;
//        guest = guestManager.CreateGuest(correct);

//        Setting();

//        // �ȳ� ��ȭ ���
//        dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 0));
//    }

//    // �ſ���(Ŭ�����) �� ��ǥ ����
//    private void Setting()
//    {
//        List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();   // �̸�, ����, ����, Ƽ��, ���� �ؽ�Ʈ

//        // ���谡 ������ �ź��� Ȱ��ȭ
//        localIdentity = closeUpIdentity.transform.Find(guest.Local).gameObject;
//        localIdentity.SetActive(true);

//        // �ؽ�Ʈ ��������
//        // �̸�, ����, ����, Ƽ�� �ؽ�Ʈ
//        for (int i = 0; i < 4; i++)
//            textList.Add(localIdentity.transform.GetChild(i).GetComponent<TextMeshProUGUI>());

//        // ���� �ؽ�Ʈ
//        textList.Add(localIdentity.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>());

//        // ������ ���� �������� (���� ���� ������ ����)
//        stampArea = localIdentity.transform.Find("StampArea").gameObject;

//        // �ſ���(Ŭ�����)�� ǥ��
//        textList[0].text = guest.Name;
//        textList[1].text = guest.Party;
//        textList[2].text = guest.Species.ToString();
//        textList[3].text = guest.Tier.ToString();
//        textList[4].text = guest.Profession.ToString();

//        // ���� ���� �̹��� ǥ��
//        Image guestProfessionSeal = localIdentity.transform.GetChild(4).GetChild(1).GetComponent<Image>();
//        guestProfessionSeal.sprite = guest.ProfessionSeal;

//        // ��ǥ �̹��� ǥ��
//        guestTierSeal.sprite = guest.TierSeal;
//    }

//    public void UpdateDate()
//    {
//        //DataController.Instance.gameData.UpdateDate();    // ��¥ ������Ʈ
//    }

//    public void SendIdentity(bool _decision)
//    {
//        decision = _decision;   // ����/���� ���� ����
//        Destroy(identity);      // ��� �ſ��� ������Ʈ ����
//        identity = null;        // ��������� null ����
//        Destroy(stampArea.transform.GetChild(0).gameObject);   // ���� ������Ʈ ����

//        CheckComplete();
//    }

//    public void SendTierSeal()
//    {
//        Destroy(tierSeal);      // ��� �ſ��� ������Ʈ ����
//        tierSeal = null;        // ��������� null ����

//        CheckComplete();
//    }

//    // �ſ���, ��ǥ ��� �����ߴ��� Ȯ��
//    private void CheckComplete()
//    {
//        if (identity == null && tierSeal == null)     // �ſ���, ��ǥ ������Ʈ ��� ����
//        {
//            StartCoroutine(DecisionComplete());
//            localIdentity.SetActive(false); // Ŭ����� ���� �ſ��� ������Ʈ ��Ȱ��ȭ
//        }
//    }

//    private IEnumerator DecisionComplete()
//    {
//        if (decision)   // ������ ���
//        {
//            if (correct)
//            {
//                HospitalityScore.Instance.correctAnswer++;
//            }
//            else
//            {
//                HospitalityScore.Instance.wrongAnswer++;
//                billAnimator.SetTrigger("Print");   // ������ ���

//            }
//            yield return dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 1));   // ���� ��ȭ ���
//        }
//        else    // ������ ���
//        {
//            yield return dialogueManager.StartCoroutine(dialogueManager.GuestDialogueCoroutine(guest.Profession, 2));   // ���� ��ȭ ���
//        }

//        // dialogManager�� �ڷ�ƾ�� ����Ǹ� ȣ��
//        VisitGuest();
//    }
//}
