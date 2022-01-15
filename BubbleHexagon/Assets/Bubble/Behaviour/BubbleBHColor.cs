using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BubbleBHColor : BubbleBehaviour
{
    public BubbleColor color;
    public Color colorValue;
    public SpriteRenderer colorSprite;

    //public List<Bubble> bsToPop;
    public BubbleListSO bubblesToPop;

    public Animator changeColorAnim;
    public BubbleFactory factory;

    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }


    public virtual void InitializeColor(ColorEnumValuePair p)
    {
        color = p.colorEnum;
        colorValue = p.colorValue;
        ApplyColor();
    }
    
    public virtual void ApplyColor()
    {
        if (colorSprite != null)
        {
            colorSprite.color = colorValue;
        }
    }

    [ContextMenu("change color")]
    public virtual void ChangeToNewColor()
    {
        changeColorAnim.Play("change");

        ColorEnumValuePair newColor = factory.GetRandomColor();
        color = newColor.colorEnum;
        colorValue = newColor.colorValue;
    }

    public override void OnSetToSlot()
    {
        List<Bubble> bsToPop = new List<Bubble>();
        List<Bubble> rainbowVisited = new List<Bubble>();
        List<Bubble> rainbowToVisit = new List<Bubble>();

        GetBubblesToPop(bsToPop);
        rainbowToVisit = bsToPop.FindAll(b => b.GetComponent<BubbleBHRainbow>() != null).Except(rainbowVisited).ToList();

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

    // 주변 찾고 4개 넘으면 여기에 넣어
    public void GetBubblesToPop(List<Bubble> bubbles)
    {
        List<Bubble> bs = new List<Bubble>();
        FindSameColor(color, bs);
        if(bs.Count >= 4)
        {
            foreach(Bubble b in bs)
            {
                if(!bubbles.Contains(b))
                {
                    bubbles.Add(b);
                }
            }
        }
    }

    public void FindSameColor(BubbleColor c, List<Bubble> bubbles)
    {
        if(!bubbles.Contains(bubble))
        {
            bubbles.Add(bubble);
        }
        foreach(Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            if(!bubbles.Contains(b))
            {
                BubbleBehaviour behav = b.GetComponent<BubbleBehaviour>();
                switch (behav)
                {
                    case BubbleBHColor behavColor:
                        if (behavColor.color == c || behavColor.color == BubbleColor.rainbow)
                        {
                            bubbles.Add(behavColor.bubble);
                            behavColor.FindSameColor(c, bubbles);
                        }
                        break;
                }
            }
        }
    }
}
