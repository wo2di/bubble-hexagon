using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Bubble itemBubble;
    public int point;
    public int maxPoint;

    public Transform fill;

    public bool HasItem()
    {
        return point >= maxPoint && itemBubble !=null;
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
            SpawnItem();
            itemBubble.gameObject.SetActive(false);
            GetComponent<Animator>().Play("itemspawn");
            SendMessageUpwards("ItemSpawned"); // play sound
        }
    }

    public void SpawnItem()
    {
        itemBubble = FindObjectOfType<BubbleFactory>().SpawnItemBubble();
        SetBubbleTransform();
    }

    public void ActivateItem()
    {
        itemBubble?.gameObject.SetActive(true);
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
        float min = -0.78f;
        float max = -0.07f;
        float p = (float)point / maxPoint;
         
        fill.localPosition = new Vector3(fill.localPosition.x, min + (max - min) * p, fill.localPosition.z);
        //fill.localScale = new Vector3(1, (float)point / maxPoint, 1);
    }

    public void SetBubbleTransform()
    {
        itemBubble.transform.localPosition = Vector3.zero;
        //itemBubble.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        itemBubble.transform.SetParent(transform, false);
    }

}
