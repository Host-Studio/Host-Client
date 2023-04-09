using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculateManager : MonoBehaviour
{
    public static CalculateManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private int date, gold_old;
    private int correct, wrong;
    // 정산 화면에서 양피지 클릭 시 호출
    public void LoadData()
    {
        // 오늘 데이터
        date = DataController.Instance.gameData.date;
        gold_old = DataController.Instance.gameData.gold;
        correct = HospitalityScore.Instance.correctAnswer;
        wrong = HospitalityScore.Instance.wrongAnswer;

        // 수입, 지출 계산
        CalculateToday();
    }

    // 정산 화면에서 
    public void UpdateGold(int gold)
    {
        DataController.Instance.gameData.UpdateGold(gold);
    }


    // 수입 항목 별 가격
    private int correctGold = 10;
    public int priceOfAccept;
    // 지출 항목 별 가격
    private int wrongGold = 10;
    public int priceOfFine;
    private void CalculateToday()
    {
        priceOfAccept = (correctGold * correct);
        priceOfFine = (wrongGold * wrong);
    }
}
