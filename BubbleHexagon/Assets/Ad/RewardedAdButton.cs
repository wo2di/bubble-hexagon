using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    //public AdmobAPI admobAPI;
    public UnityAdsAPI unityAdsAPI;

    public BoolSO isGamePaused;
    public GameplaySM gameplaySM;
    public AudioManager audioManager;

    private void OnEnable()
    {
        Debug.Log("OnEnable of rewarded ad button");
        audioManager.PlaySound("adbutton");
        unityAdsAPI.LoadAd();
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
            //admobAPI.ShowAd();
            unityAdsAPI.ShowAd();
        }
    }

}
