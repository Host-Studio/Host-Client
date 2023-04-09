using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// https://chameleonstudio.tistory.com/56 ����
public class DataController : MonoBehaviour
{

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    // J : �̱������� ����
    // J : DataContorller�� �ν��Ͻ�ȭ->�ٸ� ���Ͽ��� ��ũ��Ʈ�� ã�� �ʰ� �ٷ� ���� ����
    // J : static field, ��ü ������ ������� Ŭ�������� ����� ���� �޸𸮿� �Ҵ�ǰ� ���α׷� ���� ������ ����
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);  // J : scene�� �̵��ص� game object ����
            }
            return _instance;
        }
    }

    public string GameDataFileName = "GameData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            Debug.Log("���� ������ �ҷ����� ����!");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("���ο� ���� ������ ���� ����");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("���� ������ ���� �Ϸ�");
    }

    // ������ ��� ������ ����
    public void DeleteAllData()
    {// ���� ������ ���� ����
        File.Delete(Application.persistentDataPath + "/" + GameDataFileName);
        _gameData = null;
    }

    private void OnApplicationQuit()    // ���α׷� ���� �� ������ ����
    {
        SaveGameData();
    }
}