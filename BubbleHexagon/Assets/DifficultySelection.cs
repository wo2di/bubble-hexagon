using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelection : MonoBehaviour
{
    public GameDifficultySO gameConfig;
    public Animator easyAnim;
    public Animator hardAnim;
    public Button easyButton;
    public Button hardButton;
    public BoolSO isHardmodeOpen;
    public void SetGameMode(string d)
    {
        switch (d)
        {
            case "Easy":
                gameConfig.difficulty = Difficulty.Easy;
                easyAnim.SetBool("Selected", true);
                hardAnim.SetBool("Selected", false);
                break;
            case "Hard":
                gameConfig.difficulty = Difficulty.Hard;
                hardAnim.SetBool("Selected", true);
                easyAnim.SetBool("Selected", false);
                break;
        }
    }

    private void Start()
    {
        hardButton.interactable = isHardmodeOpen.value;
        
        SetGameMode("Easy");
    }

}
