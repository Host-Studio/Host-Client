using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    public UserData UserData;

    /////////////////// public
    public void Init()
    {
        LoadData();
    }


    public void SaveGame()
    {
        var json = JsonConvert.SerializeObject(_gameInfoData);
        var path = string.Format("{0}/gameinfo_data.json", Application.persistentDataPath);
        File.WriteAllText(path, json);
    }

    public void LoadUserData(string uid)
    {
        UserData = _gameInfoData.UserDatas.Find(x => x.uid == uid);
        if (UserData == null)
            Debug.LogFormat("{0} : ���� ���� UID" , uid);
    }


    //////////////////////////////// private

    private GameInfoData _gameInfoData;

    private DataManager()
    {
    }

    private void LoadData()
    {
        var path = string.Format("{0}/gameinfo_data.json", Application.persistentDataPath);

        if (File.Exists(path))
        {
            Debug.Log("���� ����");
            var json = File.ReadAllText(path);
            _gameInfoData = JsonConvert.DeserializeObject<GameInfoData>(json);
        }
        else
        {
            Debug.Log("�ű� ���� �Դϴ�.");
            _gameInfoData = new GameInfoData();
            UserData = new UserData("0",0,0);
            _gameInfoData.UserDatas.Add(UserData);
        }
        SaveGame();
    }
}