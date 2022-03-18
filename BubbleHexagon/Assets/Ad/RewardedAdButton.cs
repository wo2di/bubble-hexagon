using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    public AdmobAPI admobAPI;
    public BoolSO isGamePaused;
    public GameplaySM gameplaySM;
    public AudioManager audioManager;

    private void OnEnable()
    {
        Debug.Log("OnEnable of rewarded ad button");
        audioManager.PlaySound("adbutton");
    }
    //private void OnMouseUpAsButton()
    //{
    //    if(!isGamePaused.value && gameplaySM.IsSafe())
    //    {
    //        admobAPI.ShowAd();
    //    }
    //}

    public void OnButtonClick()
    {
        if (!isGamePaused.value && gameplaySM.IsSafe())
        {
            admobAPI.ShowAd();
        }
    }

}
