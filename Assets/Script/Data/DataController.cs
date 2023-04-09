using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

// https://chameleonstudio.tistory.com/56 참고
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

    // J : 싱글톤으로 구현
    // J : DataContorller를 인스턴스화->다른 파일에서 스크립트를 찾지 않고 바로 접근 가능
    // J : static field, 객체 생성과 상관없이 클래스에서 선언된 순간 메모리에 할당되고 프로그램 끝날 때까지 유지
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
                DontDestroyOnLoad(_container);  // J : scene을 이동해도 game object 유지
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
            Debug.Log("게임 데이터 불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 설정 데이터 파일 생성");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("게임 데이터 저장 완료");
    }

    // 게임의 모든 데이터 삭제
    public void DeleteAllData()
    {// 게임 데이터 파일 삭제
        File.Delete(Application.persistentDataPath + "/" + GameDataFileName);
        _gameData = null;
    }

    private void OnApplicationQuit()    // 프로그램 종료 시 데이터 저장
    {
        SaveGameData();
    }
}