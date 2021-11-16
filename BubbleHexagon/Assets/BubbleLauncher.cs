using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleLauncher : MonoBehaviour
{
    [SerializeField]
    Bubble bubble;
    public void SetBubble(Bubble b)
    {
        bubble = b;
    }
    
    public bool IsEmpty()
    {
        return bubble == null;
    }
}
