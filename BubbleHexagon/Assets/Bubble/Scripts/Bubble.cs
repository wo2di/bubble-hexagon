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


    public float speed = 20;
    public bool translated = false;

    public IEnumerator Translate(Vector3[] waypoints)
    {
        foreach(Vector3 v in waypoints)
        {
            while(Vector3.Distance(transform.position, v) > 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position, v, speed * Time.deltaTime);
                yield return null;
            }
        }
        translated = true;
    }
}
