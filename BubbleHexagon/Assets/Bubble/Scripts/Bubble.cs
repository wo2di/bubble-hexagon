using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Slot slot;

    public void SetSlot(Slot s)
    {
        slot = s;
        slot.bubble = this;
    }

    public void UnSlot()
    {
        slot.bubble = null;
        slot = null;
    }

    public void FitToSlot()
    {
        transform.position = slot.transform.position;
    }
}
