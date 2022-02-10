using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public BubbleBHBomb bomb;
    public GameEvent itemApplied;
    public void EndOfEffect()
    {
        itemApplied.Raise();
        Destroy(gameObject);
    }

    public void BiggestCirle()
    {
        bomb.DestroyAdjacents();
        Destroy(bomb.gameObject);
    }
}
