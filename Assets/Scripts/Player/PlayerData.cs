using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string playerName;
    public int level;
    public int exp;
    public Vector3 position;

    public const string PLAYER_DATA_KEY = "PlayerData";

    private PlayerLevel playerLevel;

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int level;
        public int exp;
        public Vector3 position;
    }

    private void Start()
    {
        playerLevel = GetComponent<PlayerLevel>();
    }

    private void Update()
    {
        level = playerLevel.level;
        exp = playerLevel.currentExp;
        position = transform.position;
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        SaveByPlayerPrefs();
    }

    public void Load()
    {
        LoadFromPlayerPrefs();
    }

    void SaveByPlayerPrefs()
    {
        SaveData saveData = new SaveData();

        saveData.playerName = playerName;
        saveData.level = level;
        saveData.exp = exp;
        saveData.position = position;

        SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, saveData);
    }

    void LoadFromPlayerPrefs()
    {
        var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        playerName = saveData.playerName;
        playerLevel.level = saveData.level;
        playerLevel.currentExp = saveData.exp;
        transform.position = saveData.position;

        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
