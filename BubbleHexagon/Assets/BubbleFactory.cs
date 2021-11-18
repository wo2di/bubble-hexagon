using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleFactory : MonoBehaviour
{
    public ColorTheme colorTheme;
    public ProgressConfiguration config;
    public BubbleParent bubbleParent;
    public BubblesToPrefabsSO bubbleList;

    public Bubble SpawnBubble(string bname)
    {
        Bubble obj = Instantiate(bubbleList.GetPrefabByName(bname)).GetComponent<Bubble>();
        Debug.Log("Instantiated");
        obj.transform.SetParent(bubbleParent.transform);

        switch(bname)
        {
            
            case "change":
                obj.GetComponent<BubbleBHChange>().SetColor(GetRandomColors(2));
                break;
            case "color":
            case "lock":
            case "zombie":
                obj.GetComponent<BubbleBHColor>().SetColor(GetRandomColor());
                break;
            case "rainbow":
            case "brick":
            case "spread":
                break;

        }

        return obj;
    }

    public Bubble SpawnRandomColorBubble()
    {
        Bubble b = SpawnBubble("color");
        b.GetComponent<BubbleBHColor>().SetColor(GetRandomColor());
        return b;
    }

    public ColorEnumValuePair GetRandomColor()
    {
        return colorTheme.colors[Random.Range(0, config.colorIndex)];
    }

    public List<ColorEnumValuePair> GetRandomColors(int num)
    {
        List<ColorEnumValuePair> randomColors = new List<ColorEnumValuePair>();
        randomColors.Add(GetRandomColor());

        for(int i = 1; i < num; i++)
        {
            ColorEnumValuePair c = GetRandomColor();
            while (randomColors.Contains(c))
            {
                c = GetRandomColor();
            }
            randomColors.Add(c);
        }

        return randomColors;
    }
}
