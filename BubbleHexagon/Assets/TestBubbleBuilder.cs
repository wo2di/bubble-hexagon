using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBubbleBuilder : MonoBehaviour
{
    public Transform gridParent;
    public Slot[] slots;

    private void Start()
    {
        slots = gridParent.GetComponentsInChildren<Slot>();
    }

    [ContextMenu("Set Bubbles To Slot")]
    public void SetBubblesToSlot()
    {
        foreach(Bubble b in GetComponentsInChildren<Bubble>())
        {
            if(b.slot==null)
            {
                foreach(Slot s in slots)
                {
                    //Debug.Log(Vector3.Distance(s.transform.position, b.transform.position));
                    if (s.bubble == null && Vector3.Distance(s.transform.position, b.transform.position) < 0.2f)
                    {
                        Debug.Log("Found One");
                        b.SetSlot(s);
                        b.FitToSlot();
                        break;
                    }
                    
                }
            }
        }
    }
}
