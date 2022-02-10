using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class GameplaySM : StateMachine
{
    public BubbleListSO bubblesToPop;
    public BubbleListSO bubblesToDrop;
    public IntegerSO popCount;
    public IntegerSO dropCount;

    public BubbleFactory bubbleFactory;
    public BubbleParent bubbleParent;
    public RayCaster rayCaster;
    public BubbleBHRoot rootBubble;
    public RotateGame rotateGame;
    public GridParent gridParent;
    public ItemManager itemManager;

    public Standby standby;
    public BubblePop bubblePop;
    public ApplyItem applyItem;
    public BubbleDrop bubbleDrop;
    public ExitTurn exitTurn;
    public RotateGrid rotateGrid;

    public Statistics statistics;
    public LevelManager levelManager;
    public AudioManager audioManager;
    public Transform bubbleDroppedTR;
    public ScoreManager scoreManager;

    public bool itemApplied;

    private void Awake()
    {
        standby = new Standby(this);
        bubblePop = new BubblePop(this);
        applyItem = new ApplyItem(this);
        bubbleDrop = new BubbleDrop(this);
        exitTurn = new ExitTurn(this);
        rotateGrid = new RotateGrid(this);
    }

    protected override State GetInitialState()
    {
        return standby;
    }

    public void ItemApplied()
    {
        itemApplied = true;
    }
}
