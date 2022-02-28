using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    public AdmobAPI adTest;
    public BoolSO isGamePaused;
    private void OnMouseUpAsButton()
    {
        if(!isGamePaused.value)
        {
            adTest.ShowAd();
        }
    }

}
