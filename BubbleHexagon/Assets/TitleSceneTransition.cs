using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneTransition : MonoBehaviour
{
    public GameObject fadeinCanvas;
    public SceneTransition sceneTransition;

    public void EndOfFadeIn()
    {
        fadeinCanvas.SetActive(false);
    }
    public void EndOfFadeOut()
    {
        sceneTransition.LoadScene("GameplayScene");
    }

}
