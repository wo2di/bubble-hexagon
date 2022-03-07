using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public class LevelManager : MonoBehaviour
{
    public GameDifficultySO difficulty;

    public IntegerSO shootCnt;

    public ProgressConfiguration[] shootConfigsEasy;

    public ProgressConfiguration[] shootConfigsHard;

    ProgressConfiguration[] shootConfig;
    ProgressConfiguration[] shootConfigEmpty;

    public ScoreManager scoreManager;
    public UIManager uiManager;

    public BubbleFactory bubbleFactory;
    public void CheckShootCount()
    {

        if (shootCnt.value >= 0 && shootCnt.value <50)
        {
            bubbleFactory.shootConfig = shootConfig[0];
        }
        else if( shootCnt.value < 100)
        {
            bubbleFactory.shootConfig = shootConfig[1];
        }
        else if( shootCnt.value < 150)
        {
            bubbleFactory.shootConfig = shootConfig[2];
        }
        else if( shootCnt.value < 200)
        {
            bubbleFactory.shootConfig = shootConfig[3];
        }
        else if(shootCnt.value < 250)
        {
            bubbleFactory.shootConfig = shootConfig[4];
        }
        else if(shootCnt.value < 300)
        {
            bubbleFactory.shootConfig = shootConfig[5];
        }
        else if(shootCnt.value < 350)
        {
            bubbleFactory.shootConfig = shootConfig[6];
        }
        else
        {
            bubbleFactory.shootConfig = shootConfig[7];
        }
    }

    public void CheckDifficulty()
    {
        switch (difficulty.value)
        {
            case Difficulty.Easy:
                shootConfig = shootConfigsEasy;
                scoreManager.scoreTop = scoreManager.scoreTopEasy;
                uiManager.scoreTop = scoreManager.scoreTop;
                break;
            case Difficulty.Hard:
                shootConfig = shootConfigsHard;
                scoreManager.scoreTop = scoreManager.scoreTopHard;
                uiManager.scoreTop = scoreManager.scoreTop;
                break;
        }
    }

    private void Awake()
    {
        CheckDifficulty();
    }
}
