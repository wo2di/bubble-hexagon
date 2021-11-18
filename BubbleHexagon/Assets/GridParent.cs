using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridParent : MonoBehaviour
{
    Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    public void ResetSlotColor()
    {
        foreach(Slot s in slots)
        {
            s.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
