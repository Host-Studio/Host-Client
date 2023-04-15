using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class StoryMain : SceneMain
{
    public StoryUI storyUI;
    public RectTransform _closeUpCamera;
    public Vector3 targetPosition;  // 클로즈업 카메라 이동 좌표    // 가까이 갈 수록 클로즈업 대상 오브젝트 크게보임
    public float duration;  // 이동시간


    // 대사 인덱스
    private int _msgIdx = 0;

    private bool isCloseUp = false;

    private void Start()
    {
        if(App.instance == null)
        {
            SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
            {
                Init();
            });
            SpecDataManager.instance.Init(this);
        }

        DataManager.instance.Init();
        DataManager.instance.LoadUserData("0");
        DataManager.instance.UserData.gold++;
        DataManager.instance.SaveGame();
        Debug.Log(DataManager.instance.UserData.gold);
    }

    public override void Init(SceneParams param = null)
    {
        storyUI.Init();

        SpecDataManager.instance.DialogueDBDatas[0].id = 1;


        _dialogueDBDatas = SpecDataManager.instance.DialogueDBDatas.FindAll(x => x.group_id == 2000).ToList();
    }


    /////////////////// private
    List<DialogueDBData> _dialogueDBDatas;


    private void Update()
    {
        if (isCloseUp)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (_msgIdx >= _dialogueDBDatas.Count)
            {
                isCloseUp = true;
                StartCoroutine(CloseUpImpl());
                return;
            }
            storyUI.SetMsg(_dialogueDBDatas[_msgIdx].dialogue);
            _msgIdx++;
        }
    }


    private IEnumerator CloseUpImpl()
    {
        float delaTime = 0;
        Vector3 startPosition = _closeUpCamera.position;

        while (true)
        {
            delaTime += Time.deltaTime;
            if (delaTime > duration)
                break;

            delaTime += Time.deltaTime;
            float t = Mathf.Clamp01(delaTime / duration);
            _closeUpCamera.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        //_closeUpCamera.position = targetPosition;
        storyUI.ShowWorkBtn();
        yield return null;
    }

    public void OnClickWorkBtn()
    {
        Dispatch("onClickWorkBtn");
    }


}
