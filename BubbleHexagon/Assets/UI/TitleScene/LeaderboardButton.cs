using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardButton : MonoBehaviour
{
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

}
