using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleBHRoot : BubbleBehaviour
{
    public BubbleListSO bubblesToDrop;

    public void GetBubblesToDrop()
    {
        bubblesToDrop.bubbles = transform.parent.GetComponent<BubbleParent>().GetBubblesInGrid().Except(GetBubblesConnectedToRoot()).ToList();

        //var result = from b in transform.parent.GetComponentsInChildren<Bubble>()
        //             where b.slot != null && (!GetBubblesConnectedToRoot().Contains(b))
        //             select b;

    }

    public List<Bubble> GetBubblesConnectedToRoot()
    {
        List<Bubble> bubbles = new List<Bubble>();
        bubbles.Add(bubble);
        GetConnectedBubble(bubbles);
        return bubbles;
    }

}
