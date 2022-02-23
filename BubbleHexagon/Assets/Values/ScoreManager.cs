using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public IntegerSO score;
    public IntegerSO scoreTopEasy;
    public IntegerSO scoreTopHard;
    public IntegerSO scoreTop;
    public BoolSO isHardmodeOpen;
    public int hardmodeOpenScore;
    public GameEvent scoreChangeEvent;

    public void AddScore(int i)
    {
        score.value += i;
        scoreChangeEvent.Raise();
    }


    private void Awake()
    {
        score.Reset();
        scoreChangeEvent.Raise();
    }

    public void CheckTopScore()
    {
        if(scoreTop.value < score.value)
        {
            scoreTop.value = score.value;

            if(scoreTop.value >= hardmodeOpenScore)
            {
                isHardmodeOpen.value = true;
            }
        }
    }
}
