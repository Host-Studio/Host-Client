using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private Animator stampAnimator;
    [SerializeField]
    private GameObject closeUp; // Ŭ����� ������ �� ���
    [SerializeField]
    private GameObject closeUpIdentity, closeUpTierSeal, closeUpBook;   // Ŭ����� �ź���, ��ǥ, �߶���� ����
    [SerializeField]
    private GameObject toad;    // ���� ���� �β��� ������Ʈ
    [SerializeField]
    private Sprite toadImage1, toadImage2;  // ���� ǰ�� �β���, ���� ���� �β��� �̹���

    private bool stampActive;

    // ���(BlurPanel ������Ʈ) Ŭ�� -> Ŭ����� ����
    public void ClickBlurPanel()
    {
        closeUp.SetActive(false);

        if (closeUpIdentity.activeSelf == true) // �ź��� Ŭ����� ����
        {
            // �ź��� Ŭ����� ����
            closeUpIdentity.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Identity(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }
        else if (closeUpTierSeal.activeSelf == true)   // ��ǥ Ŭ����� ����
        {
            // ��ǥ Ŭ����� ����
            closeUpTierSeal.SetActive(false);
            GameObject.Find("Canvas").transform.Find("TierSeal(Clone)").GetComponent<SpawnObject>().ReleaseCloseUp();
        }
        else    // �߶���� ���� Ŭ����� ����
        {
            // �߶���� ���� Ŭ����� ����
            closeUpBook.SetActive(false);
        }
    }

    // �β��� Ŭ��
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

    // �߶���� ���� Ŭ��
    public void ClickBook()
    {
        Debug.Log("�߶���� ���� Ŭ��");
        closeUp.SetActive(true);
        closeUpBook.SetActive(true);
    }

    // �� Ŭ��
    public void ClickBell()
    {
        Debug.Log("�� Ŭ��");
    }

    // ��ġ Ŭ��
    public void ClickHammer()
    {
        Debug.Log("��ġ Ŭ��");
    }
}
