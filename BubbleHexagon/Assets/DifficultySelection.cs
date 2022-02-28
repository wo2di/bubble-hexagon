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

    public Image hardButtonImage; 
    public Sprite hardOpenSprite;
    public Sprite hardLockSprite;
    public GameObject notifyHardmodeCondition;
    public AudioManager audioManager;
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

    public void HardModeClicked()
    {
        if (isHardmodeOpen.value) 
        {
            SetGameMode("Hard");
            audioManager.PlaySound("uiclick");
        }
        else
        {
            if(!notifyHardmodeCondition.activeSelf)
            {
                notifyHardmodeCondition.SetActive(true);
            }
        }
    }

    private void Start()
    {
        if (isHardmodeOpen.value)
        {
            hardButtonImage.sprite = hardOpenSprite;
        }
        else
        {
            hardButtonImage.sprite = hardLockSprite;
        }
        
        SetGameMode("Easy");
    }

}
