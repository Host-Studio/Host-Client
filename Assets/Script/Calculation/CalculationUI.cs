using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculationUI : MonoBehaviour
{
    // For Checking First Calculation
    private bool isFirstCal = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenCurtain(LeftCurtain, -1));
        StartCoroutine(OpenCurtain(RightCurtain, 1));
        isFirstCal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Trigger if the parchment is clicked
    private int isSized = 1;
    private bool isOpened = true;
    public void ClickedParchment()
    {
        // Animation & 정산 내역 띄우기
        StartCoroutine(Animate(parchment, isSized));
        CalculateToday();

        // 첫번째 정산 - 정산 완료 및 골드 증감 연출
        if(!isOpened && isFirstCal)
        {
            // Set as visible
            firstSettleWindow.SetActive(true);
            StartCoroutine(CompletedSettlement(outputWindow));
            isFirstCal = false;
        }

        // Switch
        isSized = -isSized;
        isOpened = !isOpened;
    }


    [Header("Curtain UI")]
    public RectTransform LeftCurtain;
    public RectTransform RightCurtain;
    private float speedOfCurtain = 400f;
    private IEnumerator OpenCurtain(RectTransform curtain, int dir)
    {
        bool isStart = true;
        while(true)
        {
            if(isStart == true)
            {
                curtain.anchoredPosition += 
                        new Vector2(dir * speedOfCurtain * Time.deltaTime, 0);
                if(Mathf.Abs(curtain.anchoredPosition.x) > 380)
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }

    [Header("Parchment UI")]
    public RectTransform parchment;
    public GameObject openedImg;
    public GameObject openedWindow;
    private IEnumerator Animate(RectTransform parchment, int dir)
    {
        bool isStart = true;
        bool isPosY = true;
        while(true)
        {
            if(isStart == true)
            {
                // Scale Up
                parchment.sizeDelta += 
                        new Vector2(dir * speedOfCurtain * Time.deltaTime, dir * speedOfCurtain * Time.deltaTime);
                // Roatate
                parchment.transform.Rotate(new Vector3(0,0, dir * 50f * Time.deltaTime));
                // Translate
                if(isPosY == true)
                {
                    parchment.anchoredPosition += 
                        new Vector2(0, dir * speedOfCurtain * Time.deltaTime);

                }
                if(parchment.anchoredPosition.y > 0 || parchment.anchoredPosition.y < -365)    // Translate until
                {
                    isPosY = false;
                }

                // Scale up and rotate until
                if((parchment.sizeDelta.x > 500 && parchment.rotation.z > 0) || 
                (parchment.sizeDelta.x < 91))
                {
                    isStart = false;
                    break;
                }
            }
            yield return null;
        }
    }

    [Header("Completed Settlement UI")]
    public GameObject firstSettleWindow;
    public GameObject outputWindow;
    public TMP_Text outputNum;
    public TMP_Text gold;
    private IEnumerator CompletedSettlement(GameObject outputWindow)
    {
        // Calcuate Gross Output
        if(output > 0)
            outputNum.text = "+" + output.ToString();
        else outputNum.text = output.ToString();
        yield return new WaitForSeconds(2.0f);

        // Translation & Hide
        outputWindow.GetComponent<Animator>().SetTrigger("Complete");
        outputWindow.SetActive(false);

        // Update Gold
        int updatedGold = (int.Parse(gold.text) + output);
        gold.text = updatedGold.ToString();
        CalculateManager.Instance.UpdateGold(updatedGold);

        // Wait 2s and set as invisible
        yield return new WaitForSeconds(2.0f);
        outputWindow.SetActive(true);
        firstSettleWindow.SetActive(false);
        yield return null;
    }

    
    [Header("Texts of Calculation")]
    public TMP_Text priceOfAccept;
    public TMP_Text priceOfFine;
    public TMP_Text sumOfIncome, sumOfOutcome;
    private int output;
    private void CalculateToday()
    {
        // Load Data from today's work
        CalculateManager.Instance.LoadData();
        
        // Open Window
        openedImg.SetActive(isOpened);
        openedWindow.SetActive(isOpened);

        int temp, income = 0, outcome = 0;
        // Calculate Income
        temp = CalculateManager.Instance.priceOfAccept;
        income += temp;     priceOfAccept.text = temp.ToString();

        sumOfIncome.text = income.ToString();
        // Calculate Outcome
        temp = CalculateManager.Instance.priceOfFine;
        outcome += temp;    priceOfFine.text = temp.ToString();

        sumOfOutcome.text = outcome.ToString();
        // Final output
        output = income - outcome;

    }
}
