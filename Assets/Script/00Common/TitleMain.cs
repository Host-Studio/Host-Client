using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMainBase
{
    public override void Init(SceneParams param = null)
    {
        StartCoroutine(this.WaitForClick());

    }

    private IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        this.StopAllCoroutines();
        App.instance.LoadScene<StoryMain>(App.eSceneType.Story);
    }
}
