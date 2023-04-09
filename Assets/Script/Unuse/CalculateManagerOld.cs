using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculateManagerOld : MonoBehaviour
{
    private bool update;    // ��� ������Ʈ �ߴ��� ����
    private int correctGold = 10, wrongGold = 10;

    [SerializeField]
    private TextMeshProUGUI dateText, correctText, wrongText, goldText;
    [SerializeField]
    private GameObject hospitalityButton;   // ����ȭ�� �̵� ��ư

    // Start is called before the first frame update
    void Start()
    {
        dateText.text = DataController.Instance.gameData.date.ToString();
        correctText.text = HospitalityScore.Instance.correctAnswer.ToString();
        wrongText.text = HospitalityScore.Instance.wrongAnswer.ToString();
        goldText.text = DataController.Instance.gameData.gold.ToString();
    }

    // ����ȭ�� �̵� ��ư Ŭ��
    public void ClickHospitality()
    {
        SceneManager.LoadScene("Hospitality");
    }

    // �ǳ� Ŭ��
    public void ClickPanel()
    {
        if (!update)
        {
            UpdateGold();
            update = !update;
            hospitalityButton.SetActive(true);  // ����ȭ�� �̵� ��ư Ȱ��ȭ
        }
    }

    // ��� ����
    private void UpdateGold()
    {
        int gold = HospitalityScore.Instance.correctAnswer * correctGold - HospitalityScore.Instance.wrongAnswer * wrongGold; // ��� ������
        DataController.Instance.gameData.UpdateGold(gold);                  // ��� ������Ʈ
        goldText.text = DataController.Instance.gameData.gold.ToString();   // ���ŵ� ��� ǥ��
    }
}
