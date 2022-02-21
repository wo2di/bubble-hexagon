using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHChange : BubbleBHColor
{
    public BubbleColor[] colors;
    public Color[] values;
    public SpriteRenderer[] sprites;
    public int nextColorIndex;

    public void InitializeColors(List<ColorEnumValuePair> colors)
    {
        Debug.Log("Initialized Change Bubble", gameObject);
        for(int i = 0; i<colors.Count; i++)
        {
            this.colors[i] = colors[i].colorEnum;
            values[i] = colors[i].colorValue;
        }

        color = this.colors[nextColorIndex];
        UpdateIndex();

        ApplyColor();
    }

    public override void ApplyColor()
    {
        for(int i = 0; i < colors.Length; i++)
        {
            sprites[i].color = values[i];
        }
    }

    public override void ChangeToNewColor()
    {
        changeColorAnim.Play("change");

        List<ColorEnumValuePair> newColors = factory.GetRandomColors(2);
        for (int i = 0; i < newColors.Count; i++)
        {
            colors[i] = newColors[i].colorEnum;
            values[i] = newColors[i].colorValue;
        }
    }

    [ContextMenu("change color")]
    public void ChangeColor()
    {
        color = colors[nextColorIndex];

        sprites[(nextColorIndex + 1) % colors.Length].GetComponent<Animator>().Play("shrink");
        sprites[nextColorIndex].GetComponent<Animator>().Play("grow");

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
