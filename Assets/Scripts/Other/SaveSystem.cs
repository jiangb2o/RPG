using System.IO;
using UnityEngine;

public static class SaveSystem
{
    #region PlayerPrefs
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
    #endregion

    #region  JSON

    public static void SaveByJson(string saveFileName, object data)
    {
        string json = JsonUtility.ToJson(data);
        // Application.presistentDataPath : 随平台变化路径
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        try
        {
            File.WriteAllText(path, json);

            #if UNITY_EDITOR
            Debug.Log($"Successfully saved data to {path}.");
            #endif
        }
        catch (System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.Log($"Failed to save data to {path}.\n{exception}");
            #endif
        }
    }

    public static T LoadFromJson<T>(string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        try
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }
        catch (System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.Log($"Failed to load data from {path}.\n{exception}");
            #endif

            return default;
        }
    }

    public static void DeleteSaveFile(string saveFileName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
            #if UNITY_EDITOR
            Debug.Log($"Failed to delete {path}.\n{exception}");
            #endif
        }
    }

    #endregion
}
