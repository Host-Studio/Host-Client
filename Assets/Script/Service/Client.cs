using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Client
{
    ///////////////////// public
    public Client()
    {
        GetData();
    }

    public VisitData GetVisitData(UserData userData)
    {
        VisitData visitData;

        // 길드
        if ((visitData = _MainOrSubVisitDatas
            .Find(x =>
            x.encounter_type == "길드" &&                           // <조건 충족 기준>
            x.reputation_count <= userData.reputations.Guild &&     // 조건 평판보다 현재의 평판이 높음
            x.group_id > userData.groupID)) != null)                // 이전에 진행했던 스토리가 아님
            return visitData;

        // 제국
        else if ((visitData = _MainOrSubVisitDatas
            .Find(x =>
            x.encounter_type == "제국" &&
            x.reputation_count <= userData.reputations.Empire &&
            x.group_id > userData.groupID)) != null)
            return visitData;

        // 부족
        else if ((visitData = _MainOrSubVisitDatas
            .Find(x =>
            x.encounter_type == "부족" &&
            x.reputation_count <= userData.reputations.Tribe &&
            x.group_id > userData.groupID)) != null)
            return visitData;

        // 랜덤
        List<VisitData> _tempVisitData = _visitDatas.FindAll(x => x.encounter_type == "랜덤");
        int rIdx = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(0, _tempVisitData.Count);

        return _tempVisitData[rIdx];
    }

    public PaperworkData GetPaperworkData(AdventurerData adventurerData, bool bCorrect)
    {
        PaperworkData result = _paperworkDatas.Find(x => x.id == adventurerData.paperwork_id);
        if (!bCorrect)
        {
            Debug.Log("incorrect");
        }

        return result;
    }

    public RewardData GetRewardData(VisitData visitData)
    {
        return _rewardDatas.Find(x => x.id == visitData.reward_id);
    }

    public AdventurerData GetAdventurerData(VisitData visitData)
    {
        return _adventurerDatas.Find(x => x.id == visitData.adventurer_id);
    }


    ///////////////////// private
    private List<VisitData> _visitDatas;
    private List<RewardData> _rewardDatas;
    private List<AdventurerData> _adventurerDatas;
    private List<PaperworkData> _paperworkDatas;

    private List<VisitData> _MainOrSubVisitDatas;

    private void GetData()
    {
        _visitDatas = SpecDataManager.instance.VisitDBDatas;
        _rewardDatas = SpecDataManager.instance.RewardDBDatas;
        _adventurerDatas = SpecDataManager.instance.AdventurerDBDatas;
        _paperworkDatas = SpecDataManager.instance.PaperworkDBDatas;

        _MainOrSubVisitDatas = _visitDatas.FindAll(x => x.encounter_type == "메인" || x.encounter_type == "서브");
    }
}