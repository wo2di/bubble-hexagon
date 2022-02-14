using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntegerSO : ScriptableObject
{
    public int value;
    public void Reset()
    {
        value = 0;
    }
}
