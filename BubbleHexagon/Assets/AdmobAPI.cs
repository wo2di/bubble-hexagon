using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;
using UnityEngine.Events;

public class AdmobAPI : MonoBehaviour
{
    public AdmobEventHandler admobEventHandler;
    public StringSO adStatus;
    public GameObject notifyNoAd;
    RewardedAd rewardedAd;

    void Start()
    {
        List<string> deviceIds = new List<string>();
        //deviceIds.Add("5874059908FE6295023F7261C5F00585"); //재이2
        deviceIds.Add("4ADED87CA7245DFFFFEF01995B4CE374"); //재이
        //deviceIds.Add("24FC3733A61DC4B270B7D8B7CE8093E7"); //병훈
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.Initialize(initStatus => { });
        MobileAds.SetRequestConfiguration(requestConfiguration);

        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        string unitID = "ca-app-pub-2249383838668943/3121631983";//realID

        //string unitID = "ca-app-pub-3940256099942544/5224354917";//testID
        rewardedAd = new RewardedAd(unitID);

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToLoad += HandleAdFailedToLoad;

        rewardedAd.OnAdClosed += HandleAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        adStatus.value = "Ad Failed To Load";
        adStatus.value = args.LoadAdError.GetMessage();
        //adStatus.value = args.LoadAdError.GetCause().GetMessage();
    }

    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        adStatus.value = "Ad Failed To Show";
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("user earned reward" + args.Type + (int)args.Amount);
        adStatus.value = "User Earned Reward";

        admobEventHandler.userEarnedReward = true;
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("ad closed");
        adStatus.value = "Ad Closed";
        CreateAndLoadRewardedAd();

        admobEventHandler.AdClosed = true;
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            adStatus.value = "ad loaded";
            rewardedAd.Show();
        }
        else
        {
            adStatus.value = "no ad loaded";
            notifyNoAd.SetActive(true);
            CreateAndLoadRewardedAd();
        }
    }

}
