using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseAndResume : MonoBehaviour
{
    public BoolSO gamePaused;

    private void OnEnable()
    {
        gamePaused.value = true;
    }

    private void OnDisable()
    {
        gamePaused.value = false;
    }
}
