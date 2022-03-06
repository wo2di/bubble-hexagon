using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobEventHandler : MonoBehaviour
{
    public GameEvent AdClosedEvent;
    public GameEvent UserEarnedRewardEvent;

    public bool AdClosed;
    public bool userEarnedReward;

    public IntegerSO adCoolTime;
    public int adCoolTimeMax;
    public GameObject adButton;
    public ItemManager itemManager;

    private void Update()
    {
        if(AdClosed)
        {
            AdClosedEvent.Raise();
            AdClosed = false;
        }
        if(userEarnedReward)
        {
            UserEarnedRewardEvent.Raise();
            userEarnedReward = false;
        }
        adButton.SetActive(adCoolTime.value == 0 && !itemManager.itemSlots[2].HasItem());
    }

    public void OnExitTurn()
    {
        if (adCoolTime.value > 0)
        {
            adCoolTime.value--;
        }
        //if (adCoolTime.value == 0)
        //{
        //    adButton.SetActive(true);
        //}
        //if (itemManager.itemSlots[2].HasItem())
        //{
        //    adButton.SetActive(false);
        //}
    }

    public void ResetAdCooltime()
    {
        adCoolTime.value = adCoolTimeMax;
    }


}
