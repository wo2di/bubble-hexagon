using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum Direction
{
    dir1,
    dir2,
    dir3,
    dir4,
    dir5,
    dir6
}

[Serializable]
public class DirSlotPair
{
    public Direction direction;
    public Slot slot;
    public DirSlotPair(Direction d, Slot s)
    {
        direction = d;
        slot = s;
    }
}

public class Slot : MonoBehaviour
{
    public Bubble bubble;
    public int level;

    public List<DirSlotPair> adjacents;

    public DirSlotPair GetPairByDir(Direction dir)
    {
        return adjacents.Find(p => p.direction == dir);
    }

    public List<Bubble> GetAdjacentBubbles()
    {
        var result = from p in adjacents
                     where p.slot != null && p.slot.bubble != null
                     select p.slot.bubble;
        return result.ToList();
    }
}
