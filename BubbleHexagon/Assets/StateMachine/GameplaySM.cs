using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class GameplaySM : StateMachine
{
    public BubbleFactory bubbleFactory;
    public BubbleParent bubbleParent;
    public RayCaster rayCaster;

    public Standby standby;
    public BubbleFired bubbleFired;
    public BubblePop bubblePop;
    public BubbleDrop bubbleDrop;
    public ExitTurn exitTurn;
    public RotateGrid rotateGrid;

    private void Awake()
    {
        standby = new Standby(this);
        bubbleFired = new BubbleFired(this);
        bubblePop = new BubblePop(this);
        bubbleDrop = new BubbleDrop(this);
        exitTurn = new ExitTurn(this);
        rotateGrid = new RotateGrid(this);
    }

    protected override State GetInitialState()
    {
        return standby;
    }
}
