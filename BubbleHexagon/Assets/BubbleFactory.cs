using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFactory : MonoBehaviour
{
    public BubbleListSO bubbleList;

    public GameObject SpawnBubble(string bname)
    {
        GameObject obj = Instantiate(bubbleList.GetPrefabByName(bname));
        return obj;
    }
}
