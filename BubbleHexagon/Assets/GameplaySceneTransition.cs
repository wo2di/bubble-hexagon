using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneTransition : MonoBehaviour
{
    public GameObject fadeinCanvas;
    public GameplayMenuUI gameplayMenu;
    public void EndOfFadeIn()
    {
        fadeinCanvas.SetActive(false);
    }

    public void EndOfFadeOut()
    {
        gameplayMenu.GoToTitle();
    }

}
