using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
