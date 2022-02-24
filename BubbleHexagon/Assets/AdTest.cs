using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;
using UnityEngine.Events;

public class AdTest : MonoBehaviour
{
    public StringSO adStatus;
    public ItemManager itemManager;

    RewardedAd rewardedAd;
    
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {

        rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToLoad += HandleAdFailedToLoad;

        rewardedAd.OnAdClosed += HandleAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void HandleAdFailedToLoad(object sender, EventArgs args)
    {
        adStatus.value = "Ad Failed To Load";
    }

    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        adStatus.value = "Ad Failed To Show";
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        adStatus.value = "User Earned Reward";
        //Debug.Log("user earned reward" + args.Type + (int) args.Amount);
        itemManager.AddOneItem();
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        adStatus.value = "Ad Closed";
        CreateAndLoadRewardedAd();
    }

    public void ShowAd()
    {
        if(rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            CreateAndLoadRewardedAd();
            Debug.Log("No Ad Loaded");
        }
    }

}
