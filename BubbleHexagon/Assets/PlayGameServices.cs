using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class PlayGameServices : MonoBehaviour
{
    public StringSO status;
    public BoolSO isConnectedToGooglePlayServices;
    private void Awake()
    {
        PlayGamesClientConfiguration clientConfiguration;
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(clientConfiguration);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public void SingInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            status.value = "authenticating ...";
            switch (result)
            {
                case SignInStatus.Success:
                    status.value = Social.localUser.userName + " " + Social.localUser.id;
                    isConnectedToGooglePlayServices.value = true;
                    break;
                default:
                    status.value = result.ToString();
                    isConnectedToGooglePlayServices.value = false;
                    break;
            }
        });
    }

    public void PostScoreToLeaderboard(int score)
    {
        Social.ReportScore(score, "CggI_5CO1h8QAhAA", (bool success) =>
        {
            if (success)
            {
                status.value = "score report success";
            }
            else
            {
                status.value = "score report fail";
            }
        });
    }

    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
}
