using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateMain : SceneMainBase
{
    // Start is called before the first frame update
    void Start()
    {
        if(App.instance == null)
        {
            this.Init();
        }

    }

    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        _calculateUI.Init();

    }
    [SerializeField] private CalculateUI _calculateUI;


}
