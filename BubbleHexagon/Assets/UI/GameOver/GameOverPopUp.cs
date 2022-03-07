using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    public AudioManager audioManager;
    public void PlayPopUpSound()
    {
        audioManager.PlaySound("uipop");
    }
}
