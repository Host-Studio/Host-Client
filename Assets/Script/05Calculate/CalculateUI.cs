using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateUI : UIBase
{
    public override void Init()
    {
        base.Init();

        _fragmentOfMemoryPop.OnClickClose();


        _gold.text = DataManager.instance.UserData.gold.ToString();
    }

    public void OnClickBed()
    {
        App.instance.LoadScene<StoryMain>(App.eSceneType.Story);
    }
    public void OnClickFragmentOfMemoryBtn()
    {
        _fragmentOfMemoryPop.Init();
        _fragmentOfMemoryPop.ShowPop();
    }

    [SerializeField] private FragmentOfMemoryPop _fragmentOfMemoryPop;
    [SerializeField] private Text _gold;
}
