using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public IntegerSO shootCnt;
    public IntegerSO popCnt;
    public IntegerSO dropCnt;
    public float time;
    private void Reset()
    {

        time = 0;
    }

    private void Awake()
    {
        Reset();
    }

    private void Update()
    {
        time += Time.deltaTime;
    }


}
