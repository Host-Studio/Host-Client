using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMain : SceneMain
{
    private UILoading uiLoading;
    public override void Init(SceneParams param = null)
    {
        this.uiLoading = GameObject.FindObjectOfType<UILoading>();
        uiLoading.Init();
        
        SpecDataManager.instance.onDataLoadComplete.AddListener((dataName, progress) => 
        {
            uiLoading.SetUI(dataName, progress);
        });

        SpecDataManager.instance.onDataLoadFinished.AddListener(() => 
        {
            App.instance.LoadScene<TitleMain>(App.eSceneType.Title);
        });
        SpecDataManager.instance.Init(App.instance);
    }
}
