using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public IntegerSO shootCnt;

    private void Reset()
    {
        shootCnt.Reset();
    }

    private void Awake()
    {
        Reset();
    }


}
