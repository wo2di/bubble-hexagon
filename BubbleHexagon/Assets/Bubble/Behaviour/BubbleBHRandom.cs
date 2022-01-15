using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHRandom : BubbleBehaviour
{
    BubbleFactory factory;

    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }
    public override void OnSetToSlot()
    {
        foreach(Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            switch (b.GetComponent<BubbleBehaviour>())
            {
                case BubbleBHColor behavColor:
                    behavColor.ChangeToNewColor();
                    break;
            }
        }
        bubble.Pop();
    }

}
