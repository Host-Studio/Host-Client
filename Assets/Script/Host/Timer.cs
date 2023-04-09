using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int maxTime;        // Ÿ�̸� �ð�
    private float startTime;
    private float currentTime;
    
    private bool isEnded;

    // �ʿ��� ������Ʈ
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
            timerText.text = "���� �ð� : " + (maxTime - (int)currentTime).ToString() + "��";
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
        timerText.text = "���� �ð� : " + (maxTime - (int)currentTime).ToString() + "��";
        isEnded = true;

        EndGame();
    }

    private void Reset_Timer()
    {
        startTime = Time.time;
        currentTime = 0;
        timerText.text = "���� �ð� : " + (maxTime - (int)currentTime).ToString() + "��";
        isEnded = false;
        Debug.Log("Start");
    }

    private void EndGame()
    {
        Debug.Log("End Game");
        SceneManager.LoadScene("RestingPlace");
    }
}