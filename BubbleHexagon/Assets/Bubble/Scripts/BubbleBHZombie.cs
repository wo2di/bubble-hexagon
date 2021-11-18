using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubbleBHZombie : BubbleBHColor
{
    public bool hasWing = true;

    public override void OnDrop()
    {
        bubble.UnSlot();
        List<Slot> emptySlots = new List<Slot>();
        foreach(Bubble b in FindObjectOfType<BubbleBHRoot>().GetBubblesConnectedToRoot())
        {
            var result = from p in b.slot.adjacents
                         where p.slot != null && p.slot.bubble == null
                         select p.slot;
            emptySlots = emptySlots.Union(result).ToList();
        }

        bubble.SetSlot(emptySlots[Random.Range(0, emptySlots.Count)]);
        bubble.FitToSlot();
    }
}
