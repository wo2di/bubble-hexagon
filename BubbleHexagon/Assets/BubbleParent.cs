using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
