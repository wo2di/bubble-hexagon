using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleFactory : MonoBehaviour
{
    public BubbleParent bubbleParent;
    public BubbleListSO bubbleList;

    public Bubble SpawnBubble(string bname)
    {
        Bubble obj = Instantiate(bubbleList.GetPrefabByName(bname)).GetComponent<Bubble>();
        obj.transform.SetParent(bubbleParent.transform);
        return obj;
    }
}
