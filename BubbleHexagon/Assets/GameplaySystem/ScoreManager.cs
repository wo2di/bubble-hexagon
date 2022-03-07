using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Statistics statistics;
    public IntegerSO score;
    public IntegerSO scoreTopEasy;
    public IntegerSO scoreTopHard;
    public IntegerSO scoreTop;
    public BoolSO isHardmodeOpen;
    public int hardmodeOpenScore;
    public GameEvent scoreChangeEvent;
    public GameEvent topScoreEvent;
    public GameObject hardmodeOpenCanvas;

    public void AddScore(int i)
    {
        score.value += i;
        scoreChangeEvent.Raise();
    }


    private void Awake()
    {
        scoreChangeEvent.Raise();
    }

    public void CheckTopScore()
    {
        if (scoreTop.value <= score.value && score.value != 0)
        {
            Debug.Log("Top Score");
            scoreTop.value = score.value;
            topScoreEvent.Raise();
        }
    }

    public void CheckHardmodeScore()
    {
        if(score.value > hardmodeOpenScore && !isHardmodeOpen.value)
        {
            isHardmodeOpen.value = true;
            hardmodeOpenCanvas.SetActive(true);
        }
    }
}
