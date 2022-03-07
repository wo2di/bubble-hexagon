using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playtime : MonoBehaviour
{
    public float playtime;
    Statistics statistics;

    private void Reset()
    {
        playtime = 0;
    }

    private void Awake()
    {
        Reset();
        statistics = GetComponent<Statistics>();
    }

    void Update()
    {
        playtime += Time.deltaTime;
        statistics.TotalPlaytime().value += Time.deltaTime;
    }
}
