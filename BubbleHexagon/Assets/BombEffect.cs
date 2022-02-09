using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public BubbleBHBomb bomb;

    public void EndOfEffect()
    {
        bomb.ItemApplied();
        Destroy(gameObject);
        Destroy(bomb.gameObject);
    }

    public void BiggestCirle()
    {
        bomb.DestroyAdjacents();
    }
}
