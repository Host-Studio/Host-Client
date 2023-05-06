using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongTestMain : MonoBehaviour
{
    public GameObject testGo;
    public BallardJournal ballardJournal;
    // Start is called before the first frame update
    void Start()
    {
        SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            ballardJournal.Init();
        });
        SpecDataManager.instance.Init(this);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCLickTest()
    {
        testGo.SetActive(!testGo.activeSelf);
    }

}
