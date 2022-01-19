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

    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<BubbleFactory>();
    }

    public override void OnExitTurn()
    {
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

}
