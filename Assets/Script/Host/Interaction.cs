using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Animator stampAnimator;
    [SerializeField]
    private GameObject closeUp; // 클로즈업 상태일 때 배경
    [SerializeField]
    private GameObject closeUpIdentity, closeUpTierSeal, closeUpBook;   // 클로즈업 신분증, 증표, 발라드의 일지
    [SerializeField]
    private GameObject toad;    // 도장 가진 두꺼비 오브젝트
    [SerializeField]
    private Sprite toadImage1, toadImage2;  // 도장 품은 두꺼비, 도장 뱉은 두꺼비 이미지

    private bool stampActive;

    // 배경(BlurPanel 오브젝트) 클릭 -> 클로즈업 해제
    public void ClickBlurPanel()
    {
        closeUp.SetActive(false);

        if (closeUpIdentity.activeSelf == true) // 신분증 클로즈업 상태
        {
            // 신분증 클로즈업 해제
            closeUpIdentity.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Identity(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }
        else if (closeUpTierSeal.activeSelf == true)   // 증표 클로즈업 상태
        {
            // 증표 클로즈업 해제
            closeUpTierSeal.SetActive(false);
            GameObject.Find("Canvas").transform.Find("TierSeal(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }
        else    // 발라드의 일지 클로즈업 상태
        {
            // 발라드의 일지 클로즈업 해제
            closeUpBook.SetActive(false);
        }
    }

    // 두꺼비 클릭
    public void ClickToad()
    {
        if (stampActive)
        {
            stampAnimator.SetBool("appear", false);
            toad.GetComponent<Image>().sprite = toadImage1;
        }
        else
        {
            stampAnimator.SetBool("appear", true);
            toad.GetComponent<Image>().sprite = toadImage2;
        }
        stampActive = !stampActive;
    }

    // 발라드의 일지 클릭
    public void ClickBook()
    {
        Debug.Log("발라드의 일지 클릭");
        closeUp.SetActive(true);
        closeUpBook.SetActive(true);
    }

    // 벨 클릭
    public void ClickBell()
    {
        Debug.Log("벨 클릭");
    }

    // 망치 클릭
    public void ClickHammer()
    {
        Debug.Log("망치 클릭");
    }
}
