using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

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

public class SaveAndLoad : MonoBehaviour
{
    public Transform bubbleParent;

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

    [ContextMenu("SAVE")]
    public void SaveSequence()
    {
        GameToData();
        SaveData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
