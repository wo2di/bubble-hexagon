using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public BoolSO soundBool;
    public Sprite soundOn;
    public Sprite soundOff;
    public Image image;

    private void Update()
    {
        if(soundBool.value)
        {
            image.sprite = soundOn;
        }
        else
        {
            image.sprite = soundOff;
        }
    }
}
