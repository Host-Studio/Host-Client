using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMain : SceneMainBase
{
    public override void Init(SceneParams param = null)
    {
        this.useOnDestoryEvent = false;

        StartCoroutine(this.ShowLogoRoutine());
    }

    private IEnumerator ShowLogoRoutine()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        App.instance.LoadScene<LoadingMain>(App.eSceneType.Loading);
    }
}