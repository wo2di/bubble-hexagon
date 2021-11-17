using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleFactory : MonoBehaviour
{
    public ColorTheme colorTheme;

    public BubbleParent bubbleParent;
    public BubbleListSO bubbleList;

    public Bubble SpawnBubble(string bname)
    {
        Bubble obj = Instantiate(bubbleList.GetPrefabByName(bname)).GetComponent<Bubble>();
        obj.transform.SetParent(bubbleParent.transform);
        return obj;
    }

    public Bubble SpawnRandomColorBubble()
    {
        Bubble b = SpawnBubble("color");
        b.GetComponent<BubbleBHColor>().SetColor(colorTheme.colors[Random.Range(0,3)]);
        return b;
    }
}
