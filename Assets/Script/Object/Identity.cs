using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Identity : MonoBehaviour
{
    public bool sealing;   // ������ �������� ����
    public bool permit;    // ���� ����

    public void SetPermit(bool _permit)
    {
        permit = _permit;
    }

    // ���� ��� (������ ��� ���̸� false, �̹� ������ �����ٸ� true ����)
    public bool Sealing()
    {
        if (!sealing)
        {
            sealing = !sealing;
            return !sealing;
        }
        return sealing;
    }
}