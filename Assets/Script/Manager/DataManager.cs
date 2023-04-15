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
        var json = JsonConvert.SerializeObject(_gameData);
        var path = string.Format("{0}/game_data.json", Application.persistentDataPath);
        File.WriteAllText(path, json);
    }

    public void LoadUserData(string uid)
    {
        UserData = _gameData.UserDatas.Find(x => x.uid == uid);
        if (UserData == null)
            Debug.LogFormat("{0} : 없는 유저 UID" , uid);
    }


    //////////////////////////////// private

    private GameData _gameData;

    private DataManager()
    {
    }

    private void LoadData()
    {
        var path = string.Format("{0}/game_data.json", Application.persistentDataPath);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            _gameData = JsonConvert.DeserializeObject<GameData>(json);
        }
        else
        {
            Debug.Log("신규 유저 입니다.");
            _gameData = new GameData();
            UserData = new UserData("0",0,0);
            _gameData.UserDatas.Add(UserData);
        }
        SaveGame();
    }
}