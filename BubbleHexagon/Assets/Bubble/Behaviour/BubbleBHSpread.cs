using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleBHSpread : BubbleBehaviour
{
    public int maxCount;
    public float spreadProbability;

    public int count;

    public Color[] colors;
    public SpriteRenderer sprite;

    public BubbleFactory factory;

    public List<Bubble> targets;
    public Bubble target;

    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }

    public override void OnExitTurn()
    {
        targets.Clear();
        target = null;

        var result = from b in bubble.slot.GetAdjacentBubbles()
                     where b.GetComponent<BubbleBHBrick>() == null && b.GetComponent<BubbleBHSpread>() == null
                     select b;
        //List<Bubble> targets = result.ToList();
        targets = result.ToList();
        if (targets.Count > 0)
        {
            count = (count + 1) % maxCount;
            StartCoroutine(ChangePhase(count));
            if(count == 0)
            {
                if (Random.value < spreadProbability)
                {
                    //°¨¿°½ÃÅ²´Ù
                    target = targets[Random.Range(0, targets.Count)];
                    Slot targetSlot = target.slot;
                    Slot currentSlot = bubble.slot;
                    target.UnSlot();
                    //target.Delete();

                    bubble.UnSlot();
                    bubble.SetSlot(targetSlot);
                    bubble.GetComponent<Animator>().Play("shrink");
                    

                    Bubble brick = factory.SpawnBubbleInGrid("brick");
                    brick.SetSlot(currentSlot);
                    brick.FitToSlot();
                }
                else
                {
                    Slot currentSlot = bubble.slot;
                    //bubble.Delete();
                    bubble.Drop();
                    Bubble brick = factory.SpawnBubbleInGrid("brick");
                    brick.SetSlot(currentSlot);
                    brick.FitToSlot();
                }

            }
        }
        else
        {
            count = 0;
            StartCoroutine(ChangePhase(count));
        }
    }

    public IEnumerator ChangePhase(int count)
    {
        for(int i = 0; i < 24; i++)
        {
            sprite.color = Color.Lerp(sprite.color, colors[count], 0.1f);
            yield return null;
        }
        sprite.color = colors[count];
    }

    public void ApplyCount()
    {
        sprite.color = colors[count];
    }

    public void EndOfShrink()
    {
        bubble.FitToSlot();
        bubble.GetComponent<Animator>().Play("grow");
    }

    public void EndOfGrow()
    {
        target.Delete();
    }

}
