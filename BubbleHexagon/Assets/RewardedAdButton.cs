using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    public AdTest adTest;
    public BoolSO isGamePaused;
    private void OnMouseUpAsButton()
    {
        if(!isGamePaused.value)
        {
            adTest.ShowAd();
        }
    }

}
