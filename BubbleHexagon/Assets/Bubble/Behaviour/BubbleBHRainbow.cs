using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BubbleBHRainbow : BubbleBHColor
{
    public override void InitializeColor(ColorEnumValuePair p) { }
    public override void ApplyColor() { }
    public override void ChangeToNewColor() { }
    public override void OnSetToSlot()
    {
        List<Bubble> bsToPop = new List<Bubble>();
        List<Bubble> rainbowVisited = new List<Bubble>();
        List<Bubble> rainbowToVisit = new List<Bubble>();

        FindSameColor(color, rainbowToVisit);

        while (rainbowToVisit.Count > 0)
        {
            foreach (Bubble b in rainbowToVisit)
            {
                b.GetComponent<BubbleBHRainbow>().CheckAdjColorBubbleToPop(bsToPop);
                rainbowVisited.Add(b);
            }
            rainbowToVisit = bsToPop.FindAll(b => b.GetComponent<BubbleBHRainbow>() != null).Except(rainbowVisited).ToList();
        }

        bubblesToPop.bubbles.AddRange(bsToPop);
    }

    public void CheckAdjColorBubbleToPop(List<Bubble> bubbles)
    {
        foreach(Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            BubbleBehaviour behav = b.GetComponent<BubbleBehaviour>();
            switch (behav)
            {
                case BubbleBHColor behavColor:
                    behavColor.GetBubblesToPop(bubbles);
                    break;
            }
        }
    }
}
