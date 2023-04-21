using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentOfMemoryPop : MonoBehaviour
{
    /////////////////////////////////
    // public
    public void Init()
    {
        
    }
    public void ShowPop()
    {
        Show();
    }

    public void OnClickClose()
    {
        Hide();
    }


    /////////////////////////////////
    // private
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
