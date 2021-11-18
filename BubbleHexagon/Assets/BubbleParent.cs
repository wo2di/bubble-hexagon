using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleParent : MonoBehaviour
{
    public Bubble bubbleNow;
    public Transform fireTR;

    public void SetBubbleNow(Bubble b)
    {
        bubbleNow = b;
        b.transform.SetPositionAndRotation(fireTR.position, Quaternion.identity);
    }

    public void ResetBubble()
    {
        bubbleNow = null;
    }

    public List<Bubble> GetBubblesInGrid()
    {
        var result = from b in GetComponentsInChildren<Bubble>()
                     where b.slot != null
                     select b;

        return result.ToList();
    }
}
