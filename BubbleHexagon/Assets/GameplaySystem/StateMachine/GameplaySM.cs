using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
public class GameplaySM : StateMachine
{
    public BubbleListSO bubblesToPop;
    public BubbleListSO bubblesToDrop;

    public BubbleFactory bubbleFactory;
    public BubbleParent bubbleParent;
    public RayCaster rayCaster;
    public BubbleBHRoot rootBubble;
    public RotateGame rotateGame;
    public GridParent gridParent;
    public ItemManager itemManager;

    public Initialize initialize;
    public Standby standby;
    public BubblePop bubblePop;
    public ApplyItem applyItem;
    public BubbleDrop bubbleDrop;
    public ExitTurn exitTurn;
    public RotateGrid rotateGrid;
    public GameOver gameOver;

    public Statistics statistics;
    public LevelManager levelManager;
    public AudioManager audioManager;
    public Transform bubbleDroppedTR;
    public ScoreManager scoreManager;
    public BubbleTrajectory bubbleTrajectory;
    public GameEvent gameOverEvent;
    public SaveAndLoadGameplay gameplaySaveLoad;
    public SaveAndLoadPlayerData playerdataSaveLoad;
    public Arrow arrow;
    public AdmobEventHandler admobEventHandler;
    public Animator warningLine;

    public bool itemApplied;

    private void Awake()
    {
        initialize = new Initialize(this);
        standby = new Standby(this);
        bubblePop = new BubblePop(this);
        applyItem = new ApplyItem(this);
        bubbleDrop = new BubbleDrop(this);
        exitTurn = new ExitTurn(this);
        rotateGrid = new RotateGrid(this);
        gameOver = new GameOver(this);
    }

    protected override State GetInitialState()
    {
        return initialize;
    }

    public void ItemApplied()
    {
        itemApplied = true;
    }

    public bool IsSafe()
    {
        return (GetCurrentState() == "Standby" && standby.shoot == false);
    }
}
