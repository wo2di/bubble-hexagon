using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public List<Sound> sounds;
    public BoolSO soundBool;
    public Sprite soundOn;
    public Sprite soundOff;
    public Image image;


    private void TurnSoundOn()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = false;
        }
        image.sprite = soundOn;
    }

    private void TurnSoundOff()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = true;
        }
        image.sprite = soundOff;
    }

    public void OnSoundButton()
    {
        if (soundBool.value)
        {
            TurnSoundOff();
            soundBool.value = false;
        }
        else
        {
            TurnSoundOn();
            soundBool.value = true;
        }
    }

    public void ApplySoundBool()
    {
        if(soundBool.value)
        {
            TurnSoundOn();
        }
        else
        {
            TurnSoundOff();
        }
    }
}
