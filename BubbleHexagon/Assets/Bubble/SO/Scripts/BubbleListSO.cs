using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BubbleListSO : ScriptableObject
{
    public List<BubbleSO> list;

    public GameObject GetPrefabByName(string str)
    {
        return list.Find(b => b.bubbleName == str).prefab;
    }
}
