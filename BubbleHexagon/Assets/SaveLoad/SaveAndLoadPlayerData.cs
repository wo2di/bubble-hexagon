using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class PlayerData
{
    public int topScoreEasy;
    public int topScoreHard;
    public bool isHardOpen;
    public bool isFirstPlay;

    public PlayerData() 
    {
        topScoreEasy = 0;
        topScoreHard = 0;
        isHardOpen = false;
        isFirstPlay = true;
    }
}

public class SaveAndLoadPlayerData : MonoBehaviour
{
    public IntegerSO topEasySO;
    public IntegerSO topHardSO;
    public BoolSO hardOpenSO;
    public BoolSO isFirstPlaySO;

    public PlayerData data;

    public string fileName;
    string androidDatapath;
    string editorDatapath;

    private void Awake()
    {
        editorDatapath = Application.dataPath + "/SaveLoad/" + fileName + ".json";
        androidDatapath = Application.persistentDataPath + "/" + fileName + ".json";
    }
    public void Serialize()
    {
        data = new PlayerData()
        {
            topScoreEasy = topEasySO.value,
            topScoreHard = topHardSO.value,
            isHardOpen = hardOpenSO.value,
            isFirstPlay = isFirstPlaySO.value
            
        };
    }

    public void Deserialize()
    {
        topEasySO.value = data.topScoreEasy;
        topHardSO.value = data.topScoreHard;
        hardOpenSO.value = data.isHardOpen;
        isFirstPlaySO.value = data.isFirstPlay;
    }

    public void SaveData()
    {
        string data_json = JsonUtility.ToJson(data, true);
        BinaryFormatter bf = new BinaryFormatter();
#if UNITY_EDITOR
        FileStream file = File.Create(editorDatapath);
#else
        FileStream file = File.Create(androidDatapath);
#endif
        bf.Serialize(file, data_json);
        file.Close();
    }

    public void LoadData()
    {

        data = new PlayerData();
        BinaryFormatter bf = new BinaryFormatter();
#if UNITY_EDITOR
        FileStream file = File.OpenRead(editorDatapath);
#else
        FileStream file = File.OpenRead(androidDatapath);
#endif
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), data);
        file.Close();

    }

    public void SaveSequence()
    {
        Serialize();
        SaveData();
    }

    public void LoadSequence()
    {
        Debug.Log("load player data");
        try
        {
            LoadData();
        }
        catch (FileNotFoundException e)
        {
            data = new PlayerData();
            SaveData();
        }
        
        Deserialize();
    }

}
