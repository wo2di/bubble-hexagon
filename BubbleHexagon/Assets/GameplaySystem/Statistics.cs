using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public GameDifficultySO difficultySO;

    public IntegerSO shootCnt;
    public IntegerSO popCnt;
    public IntegerSO dropCnt;
    

    public IntegerSO[] topShootCnts;
    public IntegerSO[] totalShootCnts;
    public IntegerSO[] topPopCnts;
    public IntegerSO[] totalPopCnts;
    public IntegerSO[] topDropCnts;
    public IntegerSO[] totalDropCnts;
    public FloatSO[] totalPlaytimes;

    public IntegerSO TopShootCnt()
    {
        return topShootCnts[(int) difficultySO.value];
    }
    public IntegerSO TotalShootCnt()
    {
        //Debug.Log("total shoot count is " + ((int)difficultySO.value).ToString());
        return totalShootCnts[(int)difficultySO.value];
    }

    public IntegerSO TopPopCnt()
    {
        return topPopCnts[(int)difficultySO.value];
    }

    public IntegerSO TotalPopCnt()
    {
        return totalPopCnts[(int)difficultySO.value];
    }

    public IntegerSO TopDropCnt()
    {
        return topDropCnts[(int)difficultySO.value];
    }

    public IntegerSO TotalDropCnt()
    {
        return totalDropCnts[(int)difficultySO.value];
    }

    public FloatSO TotalPlaytime()
    {
        return totalPlaytimes[(int)difficultySO.value];
    }

    public void OnTopScoreEvent()
    {
        TopShootCnt().value = shootCnt.value;
        TopPopCnt().value = popCnt.value;
        TopDropCnt().value = dropCnt.value;
    }

}
