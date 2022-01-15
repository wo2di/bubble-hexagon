using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHLock : BubbleBHColor
{
    public SpriteRenderer colorSprite_s;
    public bool hasLock = true;

    public GameObject baby;

    public override void InitializeColor(ColorEnumValuePair p)
    {
        color = p.colorEnum;
        colorValue = p.colorValue;

        ApplyColor();
    }

    public override void ApplyColor()
    {
        colorSprite.color = colorValue;
        colorSprite_s.color = colorValue;
    }

    public override void ChangeToNewColor()
    {
        changeColorAnim.Play("change");

        ColorEnumValuePair newColor = factory.GetRandomColor();
        color = newColor.colorEnum;
        colorValue = newColor.colorValue;
    }

    public override void OnPop()
    {
        if(hasLock)
        {
            baby.SetActive(false);
            hasLock = false;
        }
        else
        {
            base.OnPop();
        }
    }
}
