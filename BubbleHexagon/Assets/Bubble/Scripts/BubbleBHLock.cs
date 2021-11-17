using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHLock : BubbleBHColor
{
    public SpriteRenderer colorSprite_s;
    public bool hasLock = true;

    public override void SetColor(ColorEnumValuePair p)
    {
        base.SetColor(p);
        colorSprite_s.color = p.colorValue;
    }
}
