using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Hospitality �������� ����� ����ȭ�鿡�� �����ֱ� ���� ������
public class HospitalityScore : MonoBehaviour
{
    public static HospitalityScore Instance;

    public int correctAnswer;   // ���� ����
    public int wrongAnswer;     // ���� ����

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

    // ���� ���� ��� ����
    private void Reset()
    {
        if (SceneManager.GetActiveScene().name == "Hospitality")
        {
            correctAnswer = wrongAnswer = 0;
            Debug.Log("Reset score");
        }
    }
}
