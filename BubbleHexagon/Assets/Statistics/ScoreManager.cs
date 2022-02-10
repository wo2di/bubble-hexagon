using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public IntegerSO score;
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
}
