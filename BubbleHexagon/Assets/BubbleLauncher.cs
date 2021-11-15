using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class BubbleLauncher : MonoBehaviour
{
    [SerializeField]
    Bubble bubble;
    public bool shoot = false;
    public void SetBubble(Bubble b)
    {
        bubble = b;
    }
    
    public bool IsEmpty()
    {
        return bubble == null;
    }

    public NodeState LaunchAction()
    {
        if(shoot)
        {
            return NodeState.SUCCESS;
        }
        if (Input.GetMouseButtonUp(0))
        {
            shoot = true;
            StartCoroutine(bubble.Translate(new Vector3[] { (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) }));
        }
        return NodeState.RUNNING;
    }

    public NodeState ArriveAction()
    {
        if (bubble.translated)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}
