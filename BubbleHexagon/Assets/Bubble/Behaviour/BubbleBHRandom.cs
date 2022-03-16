using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BubbleBHRandom : BubbleBehaviour
{
    BubbleFactory factory;
    public Sound randomSound;
    public List<BubbleBHColor> adjacentColorBubbles;
    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }
    public override void OnSetToSlot()
    {
        randomSound.source.Play();
        
        var result = from b in bubble.slot.GetAdjacentBubbles()
                     where b.GetComponent<BubbleBehaviour>() is BubbleBHColor
                     select b.GetComponent<BubbleBHColor>();
        adjacentColorBubbles = result.ToList();

        StartCoroutine(ChangeAdjacentColors());
    }

    public IEnumerator ChangeAdjacentColors()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (BubbleBHColor c in adjacentColorBubbles)
        {
            c.ChangeToNewColor();
            c.OnSetToSlot();
        }
        OnPop();
    }

}
