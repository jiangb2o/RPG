using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerData : MonoBehaviour
{
    public string playerName;
    public int level;
    public int exp;
    public Vector3 position;

    const string PLAYER_DATA_KEY = "PlayerData";
    const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";

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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
    }

    public void Save()
    {
        //SaveByPlayerPrefs();
        SaveByJson();
    }

    public void Load()
    {
        // 避免加载完位置后自动移动到上一个目标位置
        GetComponent<NavMeshAgent>().ResetPath();
        //LoadFromPlayerPrefs();
        LoadFromJson();
    }

    #region PlayerPrefs
    void SaveByPlayerPrefs()
    {
        SaveData saveData = SavingData();
        SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, saveData);
    }

    void LoadFromPlayerPrefs()
    {
        var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        LoadData(saveData);
    }
    #endregion

    # region JSON

    void SaveByJson()
    {
        SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
        //SaveSystem.SaveByJson($"{System.DateTime.Now: yyyy.M.dd HH-mm-ss}.sav", SavingData());
    }

    void LoadFromJson()
    {
        SaveData data = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);
        LoadData(data);
    }

    #endregion


    # region Help Functions
    private SaveData SavingData()
    {
        SaveData saveData = new SaveData();

        saveData.playerName = playerName;
        saveData.level = level;
        saveData.exp = exp;
        saveData.position = position;
        return saveData;
    }

    private void LoadData(SaveData saveData)
    {
        playerName = saveData.playerName;
        playerLevel.level = saveData.level;
        playerLevel.currentExp = saveData.exp;
        transform.position = saveData.position;
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }

    [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
    }

    [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
    public static void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    }

    #endregion
}
