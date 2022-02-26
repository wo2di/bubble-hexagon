using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;
using UnityEngine.Events;

public class AdTest : MonoBehaviour
{
    public IntegerSO adCoolTime;
    public int adCoolTimeMax;
    public GameObject adButton;

    public StringSO adStatus;
    public ItemManager itemManager;
    public SaveAndLoadGameplay saveAndLoadGameplay;


    RewardedAd rewardedAd;
    
    public void OnExitTurn()
    {
        if(adCoolTime.value > 0)
        {
            adCoolTime.value--;
        }
        if (adCoolTime.value == 0)
        {
            adButton.SetActive(true);
        }
        if(itemManager.itemSlots[2].HasItem())
        {
            adButton.SetActive(false);
        }
    }

    void Start()
    {
        List<string> deviceIds = new List<string>();
        //deviceIds.Add("4ADED87CA7245DFFFFEF01995B4CE374"); //ÀçÀÌ
        deviceIds.Add("24FC3733A61DC4B270B7D8B7CE8093E7"); //º´ÈÆ
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
        saveAndLoadGameplay.SaveGameplay();
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        adStatus.value = "Ad Closed";
        CreateAndLoadRewardedAd();
        adCoolTime.value = adCoolTimeMax;
        adButton.SetActive(false);
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
