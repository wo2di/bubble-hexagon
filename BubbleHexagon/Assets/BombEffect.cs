using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public BubbleBHBomb bomb;

    public void EndOfEffect()
    {
        Destroy(gameObject);
        Destroy(bomb.gameObject);
    }

    public void BiggestCirle()
    {
        bomb.DestroyAdjacents();
    }
}
