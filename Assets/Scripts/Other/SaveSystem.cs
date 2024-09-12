using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveByPlayerPrefs(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

        #if UNITY_EDITOR
        Debug.Log("Sucessfully saved data to PlayerPrefs");
        #endif
    }

    public static string LoadFromPlayerPrefs(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}
