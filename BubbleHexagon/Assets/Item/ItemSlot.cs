using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Bubble itemBubble;
    [Range(0,1)]
    public int point;
    public int maxPoint;

    public Transform fill;

    public bool HasItem()
    {
        return point >= maxPoint;
    }

    public void UseItem()
    {
        FindObjectOfType<BubbleParent>().ApplyItemBubble(itemBubble);
    }

    public void AddPoint(int i)
    {
        point += i;
        SetFillScale();
        if (point >= maxPoint)
        {
            GetComponent<Animator>().Play("itemspawn");
            SendMessageUpwards("ItemSpawned");
            
        }
    }

    public void SpawnItem()
    {
        itemBubble = FindObjectOfType<BubbleFactory>().SpawnItemBubble();
        SetBubbleTransform();
    }

    public void ApplyChange()
    {
        if(itemBubble != null)
        {
            SetBubbleTransform();
        }
        SetFillScale();
    }

    public void ResetSlot()
    {
        itemBubble = null;
        point = 0;
    }

    public void SetFillScale()
    {
        fill.localPosition = new Vector3(fill.localPosition.x, (((float) point / maxPoint) - 1) * 0.8f, fill.localPosition.z);
        //fill.localScale = new Vector3(1, (float)point / maxPoint, 1);
    }

    public void SetBubbleTransform()
    {
        itemBubble.transform.localPosition = Vector3.zero;
        //itemBubble.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        itemBubble.transform.SetParent(transform, false);
    }

}
