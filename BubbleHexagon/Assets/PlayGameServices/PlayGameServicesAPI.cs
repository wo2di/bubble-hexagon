using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class PlayGameServicesAPI : MonoBehaviour
{
    bool signin;
    public IntegerSO scoreTopEasy;
    public IntegerSO scoreTopHard;
    public StringSO status;

    public string leaderboardIDEasy;
    public string leaderboardIDHard;

    public static PlayGameServicesAPI instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
        PlayGamesClientConfiguration clientConfiguration;
            clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(clientConfiguration);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            leaderboardIDEasy = "CgkI-a-VtNgfEAIQAw";
            leaderboardIDHard = "CgkI-a-VtNgfEAIQBA";
            SingInToGooglePlayServices(SignInInteractivity.CanPromptOnce);
#elif UNITY_IOS
       
            leaderboardIDEasy = "bubblehexagon_topscore_easy";
            leaderboardIDHard = "bubblehexagon_topscore_hard";
            SingInToiOSGamecenter();
#endif
    }

    public void SingInToGooglePlayServices(SignInInteractivity i)
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.Authenticate(i, (result) =>
        {
            status.value = "authenticating ...";
            switch (result)
            {
                case SignInStatus.Success:
                    signin = true;
                    status.value = Social.localUser.userName + " " + Social.localUser.id;
                    break;
                default:
                    signin = false;
                    status.value = result.ToString();
                    break;
            }
        });
#endif
    }

    public void SingInToiOSGamecenter()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                status.value = Social.localUser.userName + " " + Social.localUser.id;
                signin = true;
            }
            else
            {
                status.value = success.ToString();
                signin = false;
            }
        });
    }

    public void PostScoreToLeaderboard()
    {
        if(signin)
        {
            Social.ReportScore(scoreTopEasy.value, leaderboardIDEasy, (bool success) =>
            {
                if (success) status.value = "score report success";
                else status.value = "score report fail";
            });

            if(scoreTopHard.value != 0)
            {
                Social.ReportScore(scoreTopHard.value, leaderboardIDHard, (bool success) =>
                {
                    if (success) status.value = "score report success";
                    else status.value = "score report fail";
                });
            }
            
        }
    }

    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }

}