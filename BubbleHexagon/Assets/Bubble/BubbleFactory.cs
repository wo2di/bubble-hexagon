using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFactory : MonoBehaviour
{
    public ColorTheme colorTheme;
    public ProbabilityConfiguration shootConfig;
    public ProbabilityConfiguration shootConfigEmpty;
    public ProbabilityConfiguration itemConfig;
    public BubbleParent bubbleParent;
    public BubblesToPrefabsSO bubbleList;


    public Bubble SpawnBubble(string bname)
    {
        Bubble obj = Instantiate(bubbleList.GetPrefabByName(bname)).GetComponent<Bubble>();
        InitializeBubble(obj, bname);
        return obj;
    }

    public void InitializeBubble(Bubble b, string bname)
    {
        switch (bname)
        {
            case "change":
                b.GetComponent<BubbleBHChange>().InitializeColors(GetRandomColors(2));
                break;
            case "color":
            case "lock":
            case "zombie":
                b.GetComponent<BubbleBHColor>().InitializeColor(GetRandomColor());
                break;
            case "rainbow":
            case "brick":
            case "spread":
                break;
        }
    }

    public Bubble SpawnBubbleInGrid(string bname)
    {
        Bubble obj = SpawnBubble(bname);
        obj.transform.SetParent(bubbleParent.transform);

        return obj;
    }

    public Bubble SpawnRandomBubbleWhenEmpty()
    {
        Bubble b = SpawnBubble(shootConfigEmpty.GetBubbleByProbability());
        return b;
    }

    public Bubble SpawnRandomBubble()
    {
        Bubble b = SpawnBubble(shootConfig.GetBubbleByProbability());
        return b;
    }

    public Bubble SpawnItemBubble()
    {
        Bubble b = SpawnBubble(itemConfig.GetBubbleByProbability());
        return b;
    }

    public ColorEnumValuePair GetRandomColor()
    {
        return colorTheme.colors[Random.Range(0, shootConfig.colorIndex)];
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
