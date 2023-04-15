using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpecDataManager
{
    /////////////////// public
    public static readonly SpecDataManager instance = new SpecDataManager();

    public UnityEvent<string, float> onDataLoadComplete = new UnityEvent<string, float>();
    public UnityEvent onDataLoadFinished = new UnityEvent();

    public List<CutsceneDBData> CutsceneDBDatas => _cutsceneDBDatas;
    public List<CutscenGroupData> CutscenGroupDatas => _cutscenGroupDatas;
    public List<DialogueDBData> DialogueDBDatas => _dialogueDBDatas;
    public List<VisitDBData> VisitDBDatas => _visitDBDatas;



    /////////////////// private
    private List<DatapathData> _dataPaths = new List<DatapathData>();
    private List<CutsceneDBData> _cutsceneDBDatas = new List<CutsceneDBData>();
    private List<CutscenGroupData> _cutscenGroupDatas = new List<CutscenGroupData>();
    private List<DialogueDBData> _dialogueDBDatas = new List<DialogueDBData>();
    private List<VisitDBData> _visitDBDatas = new List<VisitDBData>();

    private SpecDataManager()
    {
    }

    public void Init(MonoBehaviour mono)
    {
        var datapath = "Datas/datapath_data";
        var asset = Resources.Load<TextAsset>(datapath);
        var json = asset.text;
        var datas = JsonConvert.DeserializeObject<DatapathData[]>(json);

        _dataPaths = datas.ToList();

        if (mono == null)
            App.instance.StartCoroutine(LoadAllDataRoutine());
        else
            mono.StartCoroutine(LoadAllDataRoutine());
    }


    //public void LoadData<T>(string json) where T : RawData
    //{
    //    var datas = JsonConvert.DeserializeObject<T[]>(json);
    //    datas.ToDictionary(x => x.id).ToList().ForEach(x => dicDatas.Add(x.Key, x.Value));
    //}

    private IEnumerator LoadAllDataRoutine()
    {
        int idx = 0;

        //foreach (var data in _dataPaths)
        //{
        //    var path = string.Format("Datas/{0}", data.res_name);
        //    ResourceRequest req = Resources.LoadAsync<TextAsset>(path);
        //    yield return req;
        //    float progress = (float)(idx + 1) / _dataPaths.Count;
        //    //onDataLoadComplete.Invoke(data.res_name, progress);
        //    TextAsset asset = (TextAsset)req.asset;
        //    var typeDef = Type.GetType(data.type);
        //    GetType().GetMethod(nameof(LoadData))
        //        .MakeGenericMethod(typeDef).Invoke(this, new string[] { asset.text });

        //    var arr = JsonConvert.DeserializeObject<GameMetaData>(asset.text);

        //    idx++;
        //}

        var path = string.Format("Datas/{0}", _dataPaths[0].res_name);
        ResourceRequest req = Resources.LoadAsync<TextAsset>(path);
        TextAsset asset = (TextAsset)req.asset;
        _cutsceneDBDatas = JsonConvert.DeserializeObject<CutsceneDBData[]>(asset.text).ToList();

        path = string.Format("Datas/{0}", _dataPaths[1].res_name);
        req = Resources.LoadAsync<TextAsset>(path);
        asset = (TextAsset)req.asset;
        _cutscenGroupDatas = JsonConvert.DeserializeObject<CutscenGroupData[]>(asset.text).ToList();

        path = string.Format("Datas/{0}", _dataPaths[2].res_name);
        req = Resources.LoadAsync<TextAsset>(path);
        asset = (TextAsset)req.asset;
        _dialogueDBDatas = JsonConvert.DeserializeObject<DialogueDBData[]>(asset.text).ToList();

        path = string.Format("Datas/{0}", _dataPaths[3].res_name);
        req = Resources.LoadAsync<TextAsset>(path);
        asset = (TextAsset)req.asset;
        _visitDBDatas = JsonConvert.DeserializeObject<VisitDBData[]>(asset.text).ToList();

        yield return null;
        this.onDataLoadFinished.Invoke();
    }
}
