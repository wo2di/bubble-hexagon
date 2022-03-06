using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    public AdmobAPI adTest;
    public BoolSO isGamePaused;
    public GameplaySM gameplaySM;
    private void OnMouseUpAsButton()
    {
        if(!isGamePaused.value && gameplaySM.GetCurrentState() == "Standby")
        {
            adTest.ShowAd();
        }
    }

}
