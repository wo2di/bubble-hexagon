using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHChange : BubbleBHColor
{
    public BubbleColor[] colors;
    public SpriteRenderer[] Sprites;
    public int nextColorIndex;

    public void SetColor(List<ColorEnumValuePair> colors)
    {
        for(int i = 0; i<colors.Count; i++)
        {
            this.colors[i] = colors[i].colorEnum;
            Sprites[i].color = colors[i].colorValue;
        }

        color = this.colors[nextColorIndex];
        UpdateIndex();
    }

    [ContextMenu("change color")]
    public void ChangeColor()
    {
        color = colors[nextColorIndex];

        Sprites[(nextColorIndex + 1) % colors.Length].GetComponent<Animator>().Play("shrink");
        Sprites[nextColorIndex].GetComponent<Animator>().Play("grow");

        UpdateIndex();
    }

    public void UpdateIndex()
    {
        nextColorIndex = (nextColorIndex + 1) % colors.Length;
    }

    public override void OnExitTurn()
    {
        ChangeColor();
    }

}
