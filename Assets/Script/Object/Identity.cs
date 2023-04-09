using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Identity : MonoBehaviour
{
    public bool sealing;   // 인장이 찍혔는지 여부
    public bool permit;    // 승인 여부

    public void SetPermit(bool _permit)
    {
        permit = _permit;
    }

    // 인장 찍기 (인장을 찍기 전이면 false, 이미 인장이 찍혔다면 true 리턴)
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