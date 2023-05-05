using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CloseUp : MonoBehaviour
{
    // 신원서 및 증표
    [SerializeField] private GameObject obCloseUp;
    [SerializeField] private GameObject obPaperwork;
    [SerializeField] private GameObject obToken;

    public void CloseUpSetAcitveTrue(string target)
    {
        obCloseUp.SetActive(true);

        if (target == "신원서") obPaperwork.SetActive(true);
        if (target == "증표") obToken.SetActive(true);
    }

    public void CloseUpSetAcitveFalse(string target)
    {
        if (target == "신원서") obPaperwork.SetActive(false);
        if (target == "증표") obToken.SetActive(false);

        obCloseUp.SetActive(false);
    }
}
