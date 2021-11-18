using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BubbleBehaviour : MonoBehaviour
{
    public Bubble bubble;

    protected virtual void Awake()
    {
        bubble = GetComponent<Bubble>();
    }

    public int GetSlotIndex()
    {
        return bubble.slot != null ? bubble.slot.transform.GetSiblingIndex() : 0;
    }

    public virtual void OnSetToSlot() { }

    public virtual void OnExitTurn() { }
    public virtual void OnPop() 
    {
        bubble.Pop();
    }
    public virtual void OnDrop() 
    {
        bubble.Drop();
    }

    public void GetConnectedBubble(List<Bubble> bubbles)
    {
        foreach (Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            if (!bubbles.Contains(b))
            {
                bubbles.Add(b);
                b.GetComponent<BubbleBehaviour>().GetConnectedBubble(bubbles);
            }
        }
    }
}
