using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBHBomb : BubbleBehaviour
{
    public GameObject bombEffect;
    public Sound bombSound;
    public ScoreManager scoreManager;

    protected override void Awake()
    {
        base.Awake();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public override void OnSetToSlot()
    {
        bombSound.source.Play();
        GameObject obj = Instantiate(bombEffect, bubble.slot.transform.position, Quaternion.identity, transform.parent.parent.Find("Effect"));
        obj.GetComponent<BombEffect>().bomb = this;
    }

    public void DestroyAdjacents()
    {
        foreach (Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            b.Pop();
            scoreManager.AddScore(10);
        }
    }
}
