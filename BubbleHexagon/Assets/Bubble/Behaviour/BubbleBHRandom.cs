using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBHRandom : BubbleBehaviour
{
    BubbleFactory factory;
    public Sound randomSound;
    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }
    public override void OnSetToSlot()
    {
        randomSound.source.Play();
        StartCoroutine(ChangeAdjacentColors());
    }

    public IEnumerator ChangeAdjacentColors()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Bubble b in bubble.slot.GetAdjacentBubbles())
        {
            switch (b.GetComponent<BubbleBehaviour>())
            {
                case BubbleBHColor behavColor:
                    behavColor.ChangeToNewColor();
                    break;
            }
        }
        OnPop();
    }

}
