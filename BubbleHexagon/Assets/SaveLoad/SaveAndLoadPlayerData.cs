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
    public bool musicOn;
    public bool soundOn;

    public int[] topShoots;
    public int[] totalShoots;
    public int[] topPops;
    public int[] totalPops;
    public int[] topDrops;
    public int[] totalDrops;
    public float[] totalPlaytimes;



    public PlayerData() 
    {
        topScoreEasy = 0;
        topScoreHard = 0;
        isHardOpen = false;
        isFirstPlay = true;
        musicOn = true;
        soundOn = true;

        topShoots = new int[2];
        totalShoots = new int[2];
        topPops = new int[2];
        totalPops = new int[2];
        topDrops = new int[2];
        totalDrops = new int[2];
        totalPlaytimes = new float[2];

    }
}

public class SaveAndLoadPlayerData : MonoBehaviour
{
    public IntegerSO topEasySO;
    public IntegerSO topHardSO;
    public BoolSO hardOpenSO;
    public BoolSO isFirstPlaySO;

    public BoolSO musicOnSO;
    public BoolSO soundOnSO;

    public Statistics statistics;

    public PlayerData data;

    public string fileName;
    string androidDatapath;
    string editorDatapath;

    private void Awake()
    {
        editorDatapath = Application.dataPath + "/SaveLoad/" + fileName + ".json";
        androidDatapath = Application.persistentDataPath + "/" + fileName + ".json";

        LoadSequence();
    }

    public void Serialize()
    {
        data = new PlayerData()
        {
            topScoreEasy = topEasySO.value,
            topScoreHard = topHardSO.value,
            isHardOpen = hardOpenSO.value,
            isFirstPlay = isFirstPlaySO.value,
            musicOn = musicOnSO.value,
            soundOn = soundOnSO.value,
        };

        data.topShoots[0] = statistics.topShootCnts[0].value;
        data.topShoots[1] = statistics.topShootCnts[1].value;

        data.totalShoots[0] = statistics.totalShootCnts[0].value;
        data.totalShoots[1] = statistics.totalShootCnts[1].value;

        data.topPops[0] = statistics.topPopCnts[0].value;
        data.topPops[1] = statistics.topPopCnts[1].value;

        data.totalPops[0] = statistics.totalPopCnts[0].value;
        data.totalPops[1] = statistics.totalPopCnts[1].value;

        data.topDrops[0] = statistics.topDropCnts[0].value;
        data.topDrops[1] = statistics.topDropCnts[1].value;

        data.totalDrops[0] = statistics.totalDropCnts[0].value;
        data.totalDrops[1] = statistics.totalDropCnts[1].value;

        data.totalPlaytimes[0] = statistics.totalPlaytimes[0].value;
        data.totalPlaytimes[1] = statistics.totalPlaytimes[1].value;

    }

    public void Deserialize()
    {
        topEasySO.value = data.topScoreEasy;
        topHardSO.value = data.topScoreHard;
        hardOpenSO.value = data.isHardOpen;
        isFirstPlaySO.value = data.isFirstPlay;
        musicOnSO.value = data.musicOn;
        soundOnSO.value = data.soundOn;

        statistics.topShootCnts[0].value = data.topShoots[0];
        statistics.topShootCnts[1].value = data.topShoots[1];

        statistics.totalShootCnts[0].value = data.totalShoots[0];
        statistics.totalShootCnts[1].value = data.totalShoots[1];

        statistics.topPopCnts[0].value = data.topPops[0];
        statistics.topPopCnts[1].value = data.topPops[1];

        statistics.totalPopCnts[0].value = data.totalPops[0];
        statistics.totalPopCnts[1].value = data.totalPops[1];

        statistics.topDropCnts[0].value = data.topDrops[0];
        statistics.topDropCnts[1].value = data.topDrops[1];

        statistics.totalDropCnts[0].value = data.totalDrops[0];
        statistics.totalDropCnts[1].value = data.totalDrops[1];

        statistics.totalPlaytimes[0].value = data.totalPlaytimes[0];
        statistics.totalPlaytimes[1].value = data.totalPlaytimes[1];

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
