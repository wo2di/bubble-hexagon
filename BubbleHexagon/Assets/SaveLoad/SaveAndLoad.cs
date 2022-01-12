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
public class BrickBubbleSD : BubbleSD
{
    public BrickBubbleSD(int slotIndex) : base(slotIndex) { }
}

[Serializable]
public class LockBubbleSD : ColorBubbleSD
{
    public bool hasLock;
    public LockBubbleSD(int slotIndex, BubbleColor color, bool hasLock) : base(slotIndex, color) 
    {
        this.hasLock = hasLock;
    }
}

[Serializable]
public class ChangeBubbleSD : ColorBubbleSD
{
    public BubbleColor color1;
    public BubbleColor color2;

    public ChangeBubbleSD(int slotIndex, BubbleColor color, BubbleColor color1, BubbleColor color2) : base(slotIndex, color) 
    {
        this.color1 = color1;
        this.color2 = color2;
    }
}

[Serializable]
public class RainbowBubbleSD : ColorBubbleSD
{
    public RainbowBubbleSD(int slotIndex, BubbleColor color) : base(slotIndex, color) { }
}

[Serializable]
public class SpreadBubbleSD : BubbleSD
{
    public int count;
    public SpreadBubbleSD(int slotIndex, int count) : base(slotIndex) 
    {
        this.count = count;
    }
}

[Serializable]
public class ZombieBubbleSD : ColorBubbleSD
{
    public ZombieBubbleSD(int slotIndex, BubbleColor color) : base(slotIndex, color) { }
}

[Serializable]
public class GameData
{
    public List<ColorBubbleSD> ColorList;
    public List<BrickBubbleSD> BrickList;
    public List<ChangeBubbleSD> ChangeList;
    public List<LockBubbleSD> LockList;
    public List<RainbowBubbleSD> RainbowList;
    public List<SpreadBubbleSD> SpreadList;
    public List<ZombieBubbleSD> ZombieList;
    public GameData()
    {
        ColorList = new List<ColorBubbleSD>();
        BrickList = new List<BrickBubbleSD>();
        ChangeList = new List<ChangeBubbleSD>();
        LockList = new List<LockBubbleSD>();
        RainbowList = new List<RainbowBubbleSD>();
        SpreadList = new List<SpreadBubbleSD>();
        ZombieList = new List<ZombieBubbleSD>();
    }
}

#endregion

public class SaveAndLoad : MonoBehaviour
{
    public BubbleParent bubbleParent;
    public Transform gridParent;
    public BubbleFactory bubbleFactory;

    public GameData data;
    
    public void GameToData()
    {
        data = new GameData();
        foreach(Bubble b in bubbleParent.GetBubblesInGrid())
        {
            switch(b.GetComponent<BubbleBehaviour>())
            {
                case BubbleBHChange change:
                    data.ChangeList.Add(new ChangeBubbleSD(change.GetSlotIndex(), change.color, change.colors[change.nextColorIndex % 2], change.colors[(change.nextColorIndex + 1) % 2]));
                    break;
                case BubbleBHLock lockB:
                    data.LockList.Add(new LockBubbleSD(lockB.GetSlotIndex(), lockB.color, lockB.hasLock));
                    break;
                case BubbleBHZombie zombie:
                    data.ZombieList.Add(new ZombieBubbleSD(zombie.GetSlotIndex(), zombie.color));
                    break;
                case BubbleBHRainbow rainbow:
                    data.RainbowList.Add(new RainbowBubbleSD(rainbow.GetSlotIndex(), rainbow.color));
                    break;
                case BubbleBHColor color:
                    data.ColorList.Add(new ColorBubbleSD(color.GetSlotIndex(), color.color));
                    break;
                case BubbleBHBrick brick:
                    data.BrickList.Add(new BrickBubbleSD(brick.GetSlotIndex()));
                    break;
                case BubbleBHSpread spread:
                    data.SpreadList.Add(new SpreadBubbleSD(spread.GetSlotIndex(), spread.count));
                    break;
            }
        }
    }

    public void DataToGame()
    {
        foreach(ColorBubbleSD sd in data.ColorList)
        {
            Bubble b = bubbleFactory.SpawnBubble("color");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHColor>().SetColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
        }
        foreach(BrickBubbleSD sd in data.BrickList)
        {
            Bubble b = bubbleFactory.SpawnBubble("brick");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();
        }
        foreach (ChangeBubbleSD sd in data.ChangeList)
        {
            Bubble b = bubbleFactory.SpawnBubble("change");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHChange>().SetColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
            b.GetComponent<BubbleBHChange>().SetColor(new List<ColorEnumValuePair> { bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color1), bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color2) });
        }
        foreach (LockBubbleSD sd in data.LockList)
        {
            Bubble b = bubbleFactory.SpawnBubble("lock");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHLock>().hasLock = sd.hasLock;
            b.GetComponent<BubbleBHLock>().SetColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
        }
        foreach (RainbowBubbleSD sd in data.RainbowList)
        {
            Bubble b = bubbleFactory.SpawnBubble("rainbow");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();
        }
        foreach (ZombieBubbleSD sd in data.ZombieList)
        {
            Bubble b = bubbleFactory.SpawnBubble("zombie");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHZombie>().SetColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
        }
        foreach (SpreadBubbleSD sd in data.SpreadList)
        {
            Bubble b = bubbleFactory.SpawnBubble("spread");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHSpread>().count = sd.count;
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
            Debug.Log(e);
            //InitialGameData();
        }
    }

    [ContextMenu("SAVE")]
    public void SaveSequence()
    {
        GameToData();
        SaveData();
    }

    [ContextMenu("LOAD")]
    public void LoadSequence()
    {
        LoadData();
        DataToGame();
    }
}
