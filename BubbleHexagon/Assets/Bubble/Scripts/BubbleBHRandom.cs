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
                case BubbleBHChange behavChange:
                    behavChange.SetColor(factory.GetRandomColors(2));
                    break;
                case BubbleBHColor behavColor:
                    behavColor.SetColor(factory.GetRandomColor());
                    break;
            }
        }
        bubble.Pop();
    }

}
