using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBHBomb : BubbleBehaviour
{
    public override void OnSetToSlot()
    {
        foreach (Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            b.Pop();
        }
        bubble.Pop();
    }
}
