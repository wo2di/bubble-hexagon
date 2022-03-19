using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
#if UNITY_ANDROID

    public PlayGameServicesAPI playGameServicesAPI;

    private void Awake()
    {
#if UNITY_ANDROID
        playGameServicesAPI = FindObjectOfType<PlayGameServicesAPI>();
#elif UNITY_IOS
        gameObject.SetActive(false);
#endif
    }

    public void OnLeaderboardButtonClick()
    {
        playGameServicesAPI.PostScoreToLeaderboard();
        playGameServicesAPI.ShowLeaderboardUI();
    }
#endif
    }
