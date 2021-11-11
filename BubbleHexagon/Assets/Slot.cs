using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void InitializeAdj()
    {
        if(adjacents.Count == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                adjacents.Add(new DirSlotPair((Direction)i, null));
            }
        }
    }

    public DirSlotPair GetPairByDir(Direction dir)
    {
        return adjacents.Find(p => p.direction == dir);
    }
    

}
