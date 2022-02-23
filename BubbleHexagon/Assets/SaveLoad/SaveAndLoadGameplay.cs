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
public class itemSD
{
    public int point;
    public string itemName;

    public itemSD(int point, string itemName)
    {
        this.point = point;
        this.itemName = itemName;
    }
}

[Serializable]
public class GamePlayData
{
    public List<ColorBubbleSD> ColorList;
    public List<BrickBubbleSD> BrickList;
    public List<ChangeBubbleSD> ChangeList;
    public List<LockBubbleSD> LockList;
    public List<RainbowBubbleSD> RainbowList;
    public List<SpreadBubbleSD> SpreadList;
    public List<ZombieBubbleSD> ZombieList;

    public itemSD[] items;

    public List<string> bubble1;
    public List<string> bubble2;

    public int score;
    public int shootCount;
    

    public GamePlayData()
    {
        ColorList = new List<ColorBubbleSD>();
        BrickList = new List<BrickBubbleSD>();
        ChangeList = new List<ChangeBubbleSD>();
        LockList = new List<LockBubbleSD>();
        RainbowList = new List<RainbowBubbleSD>();
        SpreadList = new List<SpreadBubbleSD>();
        ZombieList = new List<ZombieBubbleSD>();

        bubble1 = new List<string>();
        bubble2 = new List<string>();

        score = 0;
        shootCount = 0;
        items = new itemSD[3];
    }
}

#endregion

public class SaveAndLoadGameplay : MonoBehaviour
{
    public BubbleParent bubbleParent;
    public Transform gridParent;
    public BubbleFactory bubbleFactory;
    public ItemManager itemManager;
    public IntegerSO scoreSO;
    public GameEvent scoreChangeEvent;
    public RotateGame rotateGame;
    public LevelManager levelManager;
    public ColorTheme colorTheme;
    public IntegerSO shootCountSO;
    public GameDifficultySO difficultySO;

    public GamePlayData data;
    string fileName;
    string androidDatapath;
    string editorDatapath;
    private void Awake()
    {
        fileName = "save_" + difficultySO.difficulty.ToString().ToLower();
        editorDatapath = Application.dataPath + "/SaveLoad/" + fileName + ".json";
        androidDatapath = Application.persistentDataPath + "/" + fileName + ".json";

    }

    private void GameToData()
    {
        data = new GamePlayData();
        foreach(Bubble b in bubbleParent.GetBubblesInGrid())
        {
            switch(b.GetComponent<BubbleBehaviour>())
            {
                case BubbleBHChange change:
                    data.ChangeList.Add(new ChangeBubbleSD(change.GetSlotIndex(), change.color, change.colors[(change.nextColorIndex + 1) % 2], change.colors[change.nextColorIndex % 2]));
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

        data.bubble1.Add(bubbleParent.bubble1.gameObject.name.Split(' ')[0].ToLower());
        switch(bubbleParent.bubble1.gameObject.GetComponent<BubbleBehaviour>())
        {
            case BubbleBHChange change:
                data.bubble1.Add(change.colors[0].ToString());
                data.bubble1.Add(change.colors[1].ToString());
                break;
            case BubbleBHColor color:
                data.bubble1.Add(color.color.ToString());
                break;
        }

        data.bubble2.Add(bubbleParent.bubble2.gameObject.name.Split(' ')[0].ToLower());
        switch (bubbleParent.bubble2.gameObject.GetComponent<BubbleBehaviour>())
        {
            case BubbleBHChange change:
                data.bubble2.Add(change.colors[0].ToString());
                data.bubble2.Add(change.colors[1].ToString());
                break;
            case BubbleBHColor color:
                data.bubble2.Add(color.color.ToString());
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            ItemSlot s = itemManager.itemSlots[i];
            data.items[i] = new itemSD(s.point, s.itemBubble?.gameObject.name.Split(' ')[0].ToLower());
        }

        data.score = scoreSO.value;
        data.shootCount = shootCountSO.value;
    }

    private void DataToGame()
    {
        foreach(ColorBubbleSD sd in data.ColorList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("color");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHColor>().InitializeColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
        }
        foreach(BrickBubbleSD sd in data.BrickList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("brick");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();
        }
        foreach (ChangeBubbleSD sd in data.ChangeList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("change");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            //b.GetComponent<BubbleBHChange>().InitializeColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
            b.GetComponent<BubbleBHChange>().nextColorIndex = 0;
            b.GetComponent<BubbleBHChange>().InitializeColors(new List<ColorEnumValuePair> { bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color1), bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color2) });
        }
        foreach (LockBubbleSD sd in data.LockList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("lock");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();
            BubbleBHLock bhLock = b.GetComponent<BubbleBHLock>();
            bhLock.hasLock = sd.hasLock;
            bhLock.ApplyLock();
            bhLock.InitializeColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));

        }
        foreach (RainbowBubbleSD sd in data.RainbowList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("rainbow");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();
        }
        foreach (ZombieBubbleSD sd in data.ZombieList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("zombie");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            b.GetComponent<BubbleBHZombie>().InitializeColor(bubbleFactory.colorTheme.colors.Find(p => p.colorEnum == sd.color));
        }
        foreach (SpreadBubbleSD sd in data.SpreadList)
        {
            Bubble b = bubbleFactory.SpawnBubbleInGrid("spread");
            b.SetSlot(gridParent.GetChild(sd.slotIndex).GetComponent<Slot>());
            b.FitToSlot();

            BubbleBHSpread bhSpread = b.GetComponent<BubbleBHSpread>();
            bhSpread.count = sd.count;
            bhSpread.ApplyCount();
        }

        for(int i = 0; i < 3; i++)
        {
            itemManager.itemSlots[i].point = data.items[i].point;
            if(data.items[i].itemName != "")
            {
                Bubble b = bubbleFactory.SpawnBubble(data.items[i].itemName);
                itemManager.itemSlots[i].itemBubble = b;
            }

            itemManager.itemSlots[i].ApplyChange();
        }

        scoreSO.value = data.score;
        scoreChangeEvent.Raise();
        
        shootCountSO.value = data.shootCount;
        levelManager.CheckShootCount();
        rotateGame.transform.Rotate(0, 0, -60 * (shootCountSO.value % 6));

        Bubble b1 = bubbleFactory.SpawnBubble(data.bubble1[0]);
        switch(b1.GetComponent<BubbleBehaviour>())
        {
            case BubbleBHChange change:
                BubbleColor color1 = (BubbleColor) System.Enum.Parse(typeof(BubbleColor), data.bubble1[1]);
                BubbleColor color2 = (BubbleColor) System.Enum.Parse(typeof(BubbleColor), data.bubble1[2]);
                change.colors[0] = color1;
                change.colors[1] = color2;
                change.color = color1;
                change.values[0] = colorTheme.GetColorByEnum(color1);
                change.values[1] = colorTheme.GetColorByEnum(color2);
                change.colorValue = change.values[0] = colorTheme.GetColorByEnum(color1);
                change.ApplyColor();
                break;
            case BubbleBHColor color:
                BubbleColor c = (BubbleColor)System.Enum.Parse(typeof(BubbleColor), data.bubble1[1]);
                color.color = c;
                color.colorValue = colorTheme.GetColorByEnum(c);
                color.ApplyColor();
                break;
        }

        Bubble b2 = bubbleFactory.SpawnBubble(data.bubble2[0]);
        switch (b2.GetComponent<BubbleBehaviour>())
        {
            case BubbleBHChange change:
                BubbleColor color1 = (BubbleColor)System.Enum.Parse(typeof(BubbleColor), data.bubble2[1]);
                BubbleColor color2 = (BubbleColor)System.Enum.Parse(typeof(BubbleColor), data.bubble2[2]);
                change.colors[0] = color1;
                change.colors[1] = color2;
                change.color = color1;
                change.values[0] = colorTheme.GetColorByEnum(color1);
                change.values[1] = colorTheme.GetColorByEnum(color2);
                change.colorValue = change.values[0] = colorTheme.GetColorByEnum(color1);
                change.ApplyColor();
                break;
            case BubbleBHColor color:
                BubbleColor c = (BubbleColor)System.Enum.Parse(typeof(BubbleColor), data.bubble2[1]);
                color.color = c;
                color.colorValue = colorTheme.GetColorByEnum(c);
                color.ApplyColor();
                break;
        }

        bubbleParent.bubble1 = b1;
        bubbleParent.bubble2 = b2;

    }

    private void SavePlayData()
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

    private void LoadPlayData()
    {
        try
        {
            data = new GamePlayData();
            BinaryFormatter bf = new BinaryFormatter();
#if UNITY_EDITOR
            FileStream file = File.OpenRead(editorDatapath);
#else
            FileStream file = File.OpenRead(androidDatapath);
#endif
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), data);
            file.Close();
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e);
        }
    }

    [ContextMenu("SAVE")]
    public void SaveGameplay()
    {
        GameToData();
        SavePlayData();
    }

    [ContextMenu("LOAD")]
    public void LoadGameplay()
    {
        LoadPlayData();
        DataToGame();
    }

    public bool CheckGameplaySave()
    {
        try
        {
#if UNITY_EDITOR
            FileStream file = File.OpenRead(editorDatapath);
#else
            FileStream file = File.OpenRead(androidDatapath);
#endif
            file.Close();
        }
        catch(FileNotFoundException e) 
        {
            return false;
        }
        return true;
    }
    
    public void DeleteGameplaySave()
    {
#if UNITY_EDITOR
        File.Delete(editorDatapath);
#else
        File.Delete(androidDatapath);
#endif
    }
}
