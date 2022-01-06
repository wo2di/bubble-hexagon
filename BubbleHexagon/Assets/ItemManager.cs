using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int fullCount;
    public BubbleParent bubbleParent;
    public ItemSlot[] itemSlots;

    public void AddPoint(int i)
    {
        int point = i;
        foreach (ItemSlot s in itemSlots)
        {
            if(!s.HasItem())
            {
                int max = s.maxPoint - s.point;
                if (point > max)
                {
                    s.AddPoint(max);
                    point -= max;
                }
                else
                {
                    s.AddPoint(point);
                    break;
                }
            }
        }
    }

    [ContextMenu("Use Item")]
    public void UseItem()
    {
        if (itemSlots[0].HasItem() && bubbleParent.bubble3 == null)
        {
            itemSlots[0].UseItem();

            for (int i = 1; i < itemSlots.Length; i++)
            {
                itemSlots[i - 1].point = itemSlots[i].point;
                itemSlots[i - 1].itemBubble = itemSlots[i].itemBubble;
                itemSlots[i].ResetSlot();
            }

            foreach (ItemSlot s in itemSlots)
            {
                s.ApplyChange();
            }
        }
    }

    [ContextMenu("Add 3 point")]
    public void TestAddPoint()
    {
        AddPoint(3);
    }
}