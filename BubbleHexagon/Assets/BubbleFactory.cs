using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleFactory : MonoBehaviour
{
    public Transform bubbleParent;

    public BubbleListSO bubbleList;
    public BubbleLauncher bubbleLauncher;

    public GameObject SpawnBubble(string bname)
    {
        GameObject obj = Instantiate(bubbleList.GetPrefabByName(bname));
        obj.transform.SetParent(bubbleParent);
        return obj;
    }

    public NodeState SpawnBubbleAction()
    {
        if (bubbleLauncher.IsEmpty())
        {
            Bubble b = SpawnBubble("color").GetComponent<Bubble>();
            b.transform.SetPositionAndRotation(bubbleLauncher.transform.position, Quaternion.identity);
            bubbleLauncher.SetBubble(b);
        }
        return NodeState.SUCCESS;
    }
}
