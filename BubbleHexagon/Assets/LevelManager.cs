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
    public Difficulty difficulty;

    public IntegerSO shootCnt;

    public ProgressConfiguration[] shootConfigsEasy;
    public ProgressConfiguration[] shootConfigsEmptyEasy;

    public ProgressConfiguration[] shootConfigsHard;
    public ProgressConfiguration[] shootConfigsEmptyHard;


    public BubbleFactory bubbleFactory;
    public void CheckShootCount()
    {
        ProgressConfiguration[] shootConfig;
        ProgressConfiguration[] shootConfigEmpty;

        if (difficulty == Difficulty.Easy)
        {
            shootConfig = shootConfigsEasy;
            shootConfigEmpty = shootConfigsEmptyEasy;
        }
        else
        {
            shootConfig = shootConfigsHard;
            shootConfigEmpty = shootConfigsEmptyHard;
        }


        switch (shootCnt.value)
        {
            case 0:
                bubbleFactory.shootConfig = shootConfig[0];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[0];
                break;
            case 50:
                bubbleFactory.shootConfig = shootConfig[1];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[1];
                break;
            case 100:
                bubbleFactory.shootConfig = shootConfig[2];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[2];
                break;
            case 150:
                bubbleFactory.shootConfig = shootConfig[3];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[3];
                break;
            case 200:
                bubbleFactory.shootConfig = shootConfig[4];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[4];
                break;
            case 250:
                bubbleFactory.shootConfig = shootConfig[5];
                bubbleFactory.shootConfigEmpty = shootConfigEmpty[5];
                break;
        }
    }
}
