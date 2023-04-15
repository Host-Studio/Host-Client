using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{
    [SerializeField] private Text _msgTxt;
    [SerializeField] private Button _workBtn;


    public void Init()
    {
        _workBtn.gameObject.SetActive(false);

    }


    public void SetMsg(string msgTxt)
    {
        _msgTxt.text = msgTxt;
    }

    public void ShowWorkBtn()
    {
        _workBtn.gameObject.SetActive(true);
    }
}
