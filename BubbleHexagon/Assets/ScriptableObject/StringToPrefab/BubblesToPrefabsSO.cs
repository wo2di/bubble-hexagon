using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BubbleToPrefab
{
    public string bubbleName;
    public GameObject prefab;
}


[CreateAssetMenu]
public class BubblesToPrefabsSO : ScriptableObject
{
    public List<BubbleToPrefab> list;

    public GameObject GetPrefabByName(string str)
    {
        return list.Find(b => b.bubbleName == str).prefab;
    }
}
