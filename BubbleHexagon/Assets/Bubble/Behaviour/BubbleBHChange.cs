using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHChange : BubbleBHColor
{
    public BubbleColor[] colors;
    public SpriteRenderer[] bigSprites;
    public SpriteRenderer[] smallSprites;
    public int index;

    public void SetColor(List<ColorEnumValuePair> colors)
    {
        for(int i = 0; i<colors.Count; i++)
        {
            this.colors[i] = colors[i].colorEnum;
            bigSprites[i].color = colors[i].colorValue;
            smallSprites[i].color = colors[i].colorValue;
        }
        ChangeColor();
    }

    public void ChangeColor()
    {
        color = colors[index];
        for(int i = 0; i < colors.Length; i++)
        {
            bigSprites[i].gameObject.SetActive(false);
            smallSprites[i].gameObject.SetActive(true);
        }
        bigSprites[index].gameObject.SetActive(true);
        smallSprites[index].gameObject.SetActive(false);
        index = (index + 1) % colors.Length;
    }

    public override void OnExitTurn()
    {
        ChangeColor();
    }

}
