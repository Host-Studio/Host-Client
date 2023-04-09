using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Hospitality 씬에서의 결과를 정산화면에서 보여주기 위한 데이터
public class HospitalityScore : MonoBehaviour
{
    public static HospitalityScore Instance;

    public int correctAnswer;   // 정답 개수
    public int wrongAnswer;     // 오답 개수

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Reset();
        DontDestroyOnLoad(gameObject);
    }

    // 접객 씬일 경우 리셋
    private void Reset()
    {
        if (SceneManager.GetActiveScene().name == "Hospitality")
        {
            correctAnswer = wrongAnswer = 0;
            Debug.Log("Reset score");
        }
    }
}
