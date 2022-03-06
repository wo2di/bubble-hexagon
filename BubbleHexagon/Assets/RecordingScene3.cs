using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingScene3 : MonoBehaviour
{
    public BubbleParent bubbleParent;

    [ContextMenu("on exit turn")]
    public void ApplyAllOnExitTurn()
    {
        foreach(Bubble b in bubbleParent.GetBubblesInGrid())
        {
            b.GetComponent<BubbleBehaviour>().OnExitTurn();
        }
    }

}
