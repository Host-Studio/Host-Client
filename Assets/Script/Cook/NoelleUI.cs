using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoelleUI : MonoBehaviour
{
    /********************
         # 1 -> # 4
    ********************/
    [Header("C/D Buttons")]
    private bool actionBtn_Noel = false;
    public GameObject cleanBtn;
    public GameObject deleteBtn;
    public void ClickedNoel()
    {
        // clean, delete 버튼 ON/OFF
        actionBtn_Noel = !actionBtn_Noel;
        cleanBtn.SetActive(actionBtn_Noel);
        deleteBtn.SetActive(actionBtn_Noel);

        
        PrintDialog();
        
    }

    
    /********************
        # 4 -> # 6
    ********************/
    public void ClickedCDBtn(GameObject button)
    {
        int cleanAll = -1;
        cleanAll = button.name=="Clean" ? 1 : 0;
        CookDataManager.Instance.CleanHistory(cleanAll);
    }

    /********************
        # CDM -> # 2
    ********************/
    [Header("Noelle Text")]
    public TMP_Text noelleText;
    private void PrintDialog()
    {
        // history 없을 때만 대화 출력
        if (!CookDataManager.Instance.hasHistory)
            noelleText.text = CookDataManager.Instance.DialogNoelle();
    }
    public void DeleteDialog()
    {
        noelleText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        PrintDialog();
    }

}
