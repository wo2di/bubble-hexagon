//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System;
//using GoogleMobileAds.Common;
//using UnityEngine.Events;

//public class AdmobAPI : MonoBehaviour
//{
//    public AdEventHandler admobEventHandler;
//    public StringSO adStatus;
//    RewardedAd rewardedAd;

//    void Start()
//    {

//        RequestConfiguration requestConfiguration = new RequestConfiguration
//            .Builder()
//            .build();
//        MobileAds.Initialize(initStatus => { });
//        MobileAds.SetRequestConfiguration(requestConfiguration);

//        CreateAndLoadRewardedAd();
//    }

//    public void CreateAndLoadRewardedAd()
//    {
//#if UNITY_ANDROID
//        string unitID = "ca-app-pub-2249383838668943/3121631983";
//#elif UNITY_IOS
//        string unitID = "ca-app-pub-2249383838668943/5222853580";
//#endif

//        rewardedAd = new RewardedAd(unitID);

//        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        rewardedAd.OnAdFailedToLoad += HandleAdFailedToLoad;

//        rewardedAd.OnAdClosed += HandleAdClosed;

//        AdRequest request = new AdRequest.Builder().Build();
//        rewardedAd.LoadAd(request);
//    }

//    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        adStatus.value = "Ad Failed To Load";
//        adStatus.value = args.LoadAdError.ToString();
//    }

//    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        adStatus.value = "Ad Failed To Show";
//    }


//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        adStatus.value = "User Earned Reward";

//        admobEventHandler.userEarnedReward = true;
//    }

//    public void HandleAdClosed(object sender, EventArgs args)
//    {
//        adStatus.value = "Ad Closed";
//        CreateAndLoadRewardedAd();

//        admobEventHandler.AdClosed = true;
//    }

//    public void ShowAd()
//    {
//        if (rewardedAd.IsLoaded())
//        {
//            adStatus.value = "ad loaded";
//            rewardedAd.Show();
//        }
//        else
//        {
//            adStatus.value = "no ad loaded";
//            CreateAndLoadRewardedAd();
//        }
//    }

//}
