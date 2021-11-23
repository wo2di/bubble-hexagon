using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BubbleColor
{
    color1,
    color2,
    color3,
    color4,
    color5,
    color6,
    color7,
    color8,
    color9,
    rainbow

}

[Serializable]
public class ColorEnumValuePair
{
    public BubbleColor colorEnum;
    public Color colorValue;
}

[CreateAssetMenu]
public class ColorTheme : ScriptableObject
{
    public List<ColorEnumValuePair> colors;
    public Color GetColorByEnum(BubbleColor c)
    {
        return colors.Find(p => p.colorEnum == c).colorValue;
    }
}
