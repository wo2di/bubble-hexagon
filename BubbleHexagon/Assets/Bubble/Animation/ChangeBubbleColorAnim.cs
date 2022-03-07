using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBubbleColorAnim : MonoBehaviour
{
    public BubbleBHColor bHColor;

    public void ChangeBubbleColor()
    {

        bHColor.ApplyColor();
    }

}
