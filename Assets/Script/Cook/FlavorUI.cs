using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavorUI : MonoBehaviour
{
    public GameObject flavorUI;
    public TMP_Text flavorText, titleText;
    public void PrintFlavor(string data, string foodname)
    {
        flavorUI.SetActive(true);
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(targetTr, Input.mousePosition, uiCamera, out screenPoint);
        // menuUITr.localPosition = screenPoint;
        flavorUI.transform.position = Input.mousePosition + new Vector3(-300, -300, 0);
        flavorText.text = data;
        titleText.text = foodname;
    }

    public void ExitFlavor()
    {
        flavorUI.SetActive(false);
    }
    
    private void Update() {
        
    }
}
