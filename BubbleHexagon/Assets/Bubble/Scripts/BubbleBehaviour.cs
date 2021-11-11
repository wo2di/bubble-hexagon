using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleBehaviour : MonoBehaviour
{
    public Bubble bubble;

    protected virtual void Awake()
    {
        bubble = GetComponent<Bubble>();
    }

    public int GetSlotIndex()
    {
        return bubble.slot != null ? bubble.slot.transform.GetSiblingIndex() : 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
