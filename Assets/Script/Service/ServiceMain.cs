using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

class ServiceMain : SceneMain
{
    private void Start()
    {
        if (App.instance == null)
        {
            SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
            {
                Init();
            });
            SpecDataManager.instance.Init(this);
        }

        dialogue = new Dialogue();
        userData = DataManager.instance.UserData;
        _visitDBDatas = SpecDataManager.instance.VisitDBDatas.ToList();
    }

    public override void Init(SceneParams param = null)
    {
        SpecDataManager.instance.DialogueDBDatas[0].id = 1;
    }

    /////////////////// private
    private Dialogue dialogue;
    private UserData userData;
    private List<VisitDBData> _visitDBDatas;

    private int MainDialgueIdx = 1000;
    private int RandomDialogueIdx = 3000;

    /*
     <필요한 로직들>

        1. NPC 랜덤 등장
        2. 플레이어 선택지에 따른 대사 전달
        3. 결과 -> UserData에 저장
        4. 시간 계산? 은 별개로 따로 빼서 하면 될듯
        5. 특정 기점마다 조건 비교... 
     */

    public void VisitGuest()
    {
        VisitDBData visitDBData = null;
        int group_id = 0;
        int dialogue_type = 0;

        // Main or Sub Guest
        if((visitDBData = HasMainOrSubGuest()) != null)
        {
            group_id = visitDBData.group_id;
            dialogue_type = visitDBData.dialogue_type;
        }

        // Random Guest
        else
        {
            group_id = dialogue.GetRandomGroupID();
            dialogue_type = 0;
        }

        dialogue.StartCoroutine(dialogue.GuestDialogueCoroutine(group_id, dialogue_type));
    }

    private VisitDBData HasMainOrSubGuest()
    {
        VisitDBData visitDBData = null;
        
        // 등장해야 하는 메인or서브 NPC가 있는지 확인
        for(int rep = 0; rep < 3; rep++)
        {
            if((visitDBData = _visitDBDatas.Find(x =>
                x.encounter_condition == rep &&
                x.condition_count <= userData.reputations[rep] &&
                x.group_id > userData.groupID)) != null)
            {
                break;
            }
        }

        return visitDBData;
    }

    private 
}
