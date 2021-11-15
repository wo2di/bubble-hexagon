using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class GameTree : MonoBehaviour
{
    public BubbleFactory bubbleFactory;
    public BubbleLauncher bubbleLauncher;

    public Sequence sequence1;
    public ActionNode action1;
    public ActionNode action2;
    public ActionNode action3;

    private void Start()
    {
        action1 = new ActionNode(bubbleFactory.SpawnBubbleAction);
        action2 = new ActionNode(bubbleLauncher.LaunchAction);
        action3 = new ActionNode(bubbleLauncher.ArriveAction);
        sequence1 = new Sequence(new List<Node>() { action1, action2, action3});
    }

    private void Update()
    {
        Debug.Log(sequence1.Evaluate());
    }


}
