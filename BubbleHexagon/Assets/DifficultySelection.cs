using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public GameConfigurationSO gameConfig;
    public Animator easyAnim;
    public Animator hardAnim;
    public void SetGameMode(string d)
    {
        switch (d)
        {
            case "Easy":
                Debug.Log("Easy Selected");
                gameConfig.difficulty = Difficulty.Easy;
                easyAnim.SetBool("Selected", true);
                hardAnim.SetBool("Selected", false);
                break;
            case "Hard":
                Debug.Log("Hard Selected");
                gameConfig.difficulty = Difficulty.Hard;
                hardAnim.SetBool("Selected", true);
                easyAnim.SetBool("Selected", false);
                break;
        }
    }

    private void Start()
    {
        SetGameMode("Easy");
    }

}
