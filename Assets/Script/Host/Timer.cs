using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int maxTime;        // 타이머 시간
    private float startTime;
    private float currentTime;
    
    private bool isEnded;

    // 필요한 컴포넌트
    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Start()
    {
        Reset_Timer();
    }

    void Update()
    {
        if (isEnded)
            return;

        Check_Timer();
    }

    //CodeFinder
    //From https://codefinder.janndk.com/
    private void Check_Timer()
    {
        currentTime = Time.time - startTime;
        if (currentTime < maxTime)
        {
            timerText.text = "남은 시간 : " + (maxTime - (int)currentTime).ToString() + "초";
            //Debug.Log(currentTime);
        }
        else if (!isEnded)
        {
            End_Timer();
        }
    }

    private void End_Timer()
    {
        Debug.Log("End");
        currentTime = maxTime;
        timerText.text = "남은 시간 : " + (maxTime - (int)currentTime).ToString() + "초";
        isEnded = true;

        EndGame();
    }

    private void Reset_Timer()
    {
        startTime = Time.time;
        currentTime = 0;
        timerText.text = "남은 시간 : " + (maxTime - (int)currentTime).ToString() + "초";
        isEnded = false;
        Debug.Log("Start");
    }

    private void EndGame()
    {
        Debug.Log("End Game");
        SceneManager.LoadScene("RestingPlace");
    }
}