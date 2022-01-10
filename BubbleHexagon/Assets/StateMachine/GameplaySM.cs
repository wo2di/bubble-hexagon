using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class GameplaySM : StateMachine
{
    public BubbleFactory bubbleFactory;
    public BubbleParent bubbleParent;
    public RayCaster rayCaster;
    public BubbleListSO bubblesToPop;
    public BubbleBHRoot rootBubble;
    public BubbleListSO bubblesToDrop;
    public RotateGame rotateGame;
    public GridParent gridParent;
    public ItemManager itemManager;

    public Standby standby;
    public BubblePop bubblePop;
    public BubbleDrop bubbleDrop;
    public ExitTurn exitTurn;
    public RotateGrid rotateGrid;
    public Statistics statistics;
    public LevelManager levelManager;

    private void Awake()
    {
        standby = new Standby(this);
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
