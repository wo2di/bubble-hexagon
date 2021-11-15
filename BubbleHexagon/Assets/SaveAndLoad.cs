using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

#region save data class

[Serializable]
public class BubbleSD
{
    public int slotIndex;
    public BubbleSD(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }
}

[Serializable]
public class ColorBubbleSD : BubbleSD
{
    public BubbleColor color;
    public ColorBubbleSD(int slotIndex, BubbleColor color) : base(slotIndex)
    {
        this.color = color;
    }
}

[Serializable]
public class GameData
{
    public List<ColorBubbleSD> ColorList;
    public GameData()
    {
        ColorList = new List<ColorBubbleSD>();
    }
}

#endregion

public class SaveAndLoad : MonoBehaviour
{
    public Transform bubbleParent;
    public Transform gridParent;
    public BubbleFactory bubbleFactory;

    public GameData data;
    

    public void GameToData()
    {
        data = new GameData();
        foreach(BubbleBehaviour b in bubbleParent.GetComponentsInChildren<BubbleBehaviour>())
        {
            switch(b)
            {
                case BubbleBHColor color:
                    data.ColorList.Add(new ColorBubbleSD(color.GetSlotIndex(), color.color));
                    break;
            }
        }
    }

    public void DataToGame()
    {
        foreach(ColorBubbleSD sd in data.ColorList)
        {
            int i = sd.slotIndex;
            BubbleColor c = sd.color;
            GameObject obj = bubbleFactory.SpawnBubble("color");
            //obj.transform.SetParent(bubbleParent);
            obj.GetComponent<Bubble>().SetSlot(gridParent.GetChild(i).GetComponent<Slot>());
            obj.GetComponent<Bubble>().FitToSlot();
            obj.GetComponent<BubbleBHColor>().SetColor(c);
            
        }
    }

    public void SaveData()
    {
        string data_json = JsonUtility.ToJson(data, true);
        BinaryFormatter bf = new BinaryFormatter();
#if UNITY_EDITOR
        FileStream file = File.Create(Application.dataPath + "/save.json");
#else
        FileStream file = File.Create(Application.persistentDataPath + "/save.json");
#endif
        bf.Serialize(file, data_json);
        file.Close();
    }

    public void LoadData()
    {
        try
        {
            //ResetGameData();
            data = new GameData();
            BinaryFormatter bf = new BinaryFormatter();
#if UNITY_EDITOR
            FileStream file = File.OpenRead(Application.dataPath + "/save.json");
#else
            FileStream file = File.OpenRead(Application.persistentDataPath + "/save.json");
#endif
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), data);
            file.Close();
        }
        catch (FileNotFoundException e)
        {
            //InitialGameData();
        }
    }

    [ContextMenu("SAVE")]
    public void SaveSequence()
    {
        GameToData();
        SaveData();
    }

    [ContextMenu("Load")]
    public void LoadSequence()
    {
        LoadData();
        DataToGame();
    }
}
