using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TycoonEditor : MonoBehaviour
{
    [MenuItem("TYCOON/gameinfo_data/delete")]
    public static void DeleteGameInfo()
    {
        var path = string.Format("{0}/gameinfo_data.json", Application.persistentDataPath);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("gameinfo_data.json deleted");
        }
        else
        {
            Debug.Log("gameinfo_data.json not found.");
        }

        // https://answers.unity.com/questions/43422/how-to-implement-show-in-explorer.html
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }

    [MenuItem("TYCOON/gameinfo_data/show in explorer")]
    public static void ShowInExplorer()
    {
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }
}
