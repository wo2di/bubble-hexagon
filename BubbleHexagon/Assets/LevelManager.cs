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
    public ProgressConfiguration[] shootConfigsEmptyEasy;

    public ProgressConfiguration[] shootConfigsHard;
    public ProgressConfiguration[] shootConfigsEmptyHard;

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
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[0];
        }
        else if( shootCnt.value < 100)
        {
            bubbleFactory.shootConfig = shootConfig[1];
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[1];
        }
        else if( shootCnt.value < 150)
        {
            bubbleFactory.shootConfig = shootConfig[2];
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[2];
        }
        else if( shootCnt.value < 200)
        {
            bubbleFactory.shootConfig = shootConfig[3];
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[3];
        }
        else if(shootCnt.value < 250)
        {
            bubbleFactory.shootConfig = shootConfig[4];
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[4];
        }
        else
        {
            bubbleFactory.shootConfig = shootConfig[5];
            bubbleFactory.shootConfigEmpty = shootConfigEmpty[5];
        }
            

        //switch (shootCnt.value)
        //{
        //    case 0:
        //        bubbleFactory.shootConfig = shootConfig[0];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[0];
        //        break;
        //    case 50:
        //        bubbleFactory.shootConfig = shootConfig[1];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[1];
        //        break;
        //    case 100:
        //        bubbleFactory.shootConfig = shootConfig[2];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[2];
        //        break;
        //    case 150:
        //        bubbleFactory.shootConfig = shootConfig[3];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[3];
        //        break;
        //    case 200:
        //        bubbleFactory.shootConfig = shootConfig[4];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[4];
        //        break;
        //    case 250:
        //        bubbleFactory.shootConfig = shootConfig[5];
        //        bubbleFactory.shootConfigEmpty = shootConfigEmpty[5];
        //        break;
        //}
    }

    public void CheckDifficulty()
    {
        switch (difficulty.difficulty)
        {
            case Difficulty.Easy:
                shootConfig = shootConfigsEasy;
                shootConfigEmpty = shootConfigsEmptyEasy;
                scoreManager.scoreTop = scoreManager.scoreTopEasy;
                uiManager.scoreTop = scoreManager.scoreTop;
                break;
            case Difficulty.Hard:
                shootConfig = shootConfigsHard;
                shootConfigEmpty = shootConfigsEmptyHard;
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
