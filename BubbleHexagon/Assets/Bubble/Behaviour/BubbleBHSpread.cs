using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleBHSpread : BubbleBehaviour
{
    public int maxCount;
    public float spreadProbability;

    public int count;

    public BubbleFactory factory;

    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }

    public override void OnExitTurn()
    {
        var result = from b in bubble.slot.GetAdjacentBubbles()
                     where b.slot.level > 1 && b.GetComponent<BubbleBHBrick>() == null && b.GetComponent<BubbleBHSpread>() == null
                     select b;
        List<Bubble> targets = result.ToList();
        if(targets.Count > 0)
        {
            count = (count + 1) % maxCount;
            if(count == 0)
            {
                if (Random.value < spreadProbability)
                {
                    //°¨¿°½ÃÅ²´Ù
                    Bubble target = targets[Random.Range(0, targets.Count)];
                    Slot targetSlot = target.slot;
                    Slot currentSlot = bubble.slot;
                    target.Delete();

                    bubble.UnSlot();
                    bubble.SetSlot(targetSlot);
                    bubble.FitToSlot();

                    Bubble brick = factory.SpawnBubbleInGrid("brick");
                    brick.SetSlot(currentSlot);
                    brick.FitToSlot();
                }
                else
                {
                    Slot currentSlot = bubble.slot;
                    bubble.Delete();
                    Bubble brick = factory.SpawnBubbleInGrid("brick");
                    brick.SetSlot(currentSlot);
                    brick.FitToSlot();
                }

            }
        }
        else
        {
            count = 0;
        }
    }
}
