using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
#if UNITY_ANDROID

    public PlayGameServicesAPI playGameServicesAPI;

    private void Awake()
    {
        playGameServicesAPI = FindObjectOfType<PlayGameServicesAPI>();
    }

    public void OnLeaderboardButtonClick()
    {
        playGameServicesAPI.PostScoreToLeaderboard();
        playGameServicesAPI.ShowLeaderboardUI();
    }
#endif
}
