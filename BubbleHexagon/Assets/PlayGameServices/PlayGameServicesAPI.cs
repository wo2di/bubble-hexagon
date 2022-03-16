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

        PlayGamesClientConfiguration clientConfiguration;
        clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(clientConfiguration);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SingInToGooglePlayServices(SignInInteractivity.CanPromptOnce);
    }

    public void SingInToGooglePlayServices(SignInInteractivity i)
    {
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
    }

    public void PostScoreToLeaderboard()
    {
        if(signin)
        {
            Social.ReportScore(scoreTopEasy.value, "CgkI-a-VtNgfEAIQAw", (bool success) =>
            {
                if (success) status.value = "score report success";
                else status.value = "score report fail";
            });

            if(scoreTopHard.value != 0)
            {
                Social.ReportScore(scoreTopHard.value, "CgkI-a-VtNgfEAIQBA", (bool success) =>
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
