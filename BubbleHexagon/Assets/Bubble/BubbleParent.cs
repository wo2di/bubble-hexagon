using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleParent : MonoBehaviour
{
    public Bubble bubble1;
    public Bubble bubble2;
    public Bubble bubble3;

    public Transform fireTR;
    public Transform nextTR;
    public float bubbleSpeed;

    public void SetBubbles()
    {
        bubble1.gameObject.SetActive(true);
        bubble2.gameObject.SetActive(true);
        if (bubble3 != null)
        {
            bubble3.gameObject.SetActive(false);
        }
        bubble1.transform.SetPositionAndRotation(fireTR.position, Quaternion.identity);
        bubble2.transform.SetPositionAndRotation(nextTR.position, Quaternion.identity);
        bubble1.transform.SetParent(fireTR);
        bubble2.transform.SetParent(nextTR);
    }

    public void ApplyItemBubble(Bubble itemBubble)
    {
        bubble3 = bubble2;
        bubble2 = bubble1;
        bubble1 = itemBubble;
        
        SetBubbles();
        
    }

    public List<Bubble> GetBubblesInGrid()
    {
        var result = from b in GetComponentsInChildren<Bubble>()
                     where b.slot != null
                     select b;

        return result.ToList();
    }
}
