using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    public SaveAndLoadPlayerData saveAndLoadPlayerData;
    public Sound[] sounds;
    public BoolSO soundBool;

    private void Start()
    {
        ApplySoundBool();
    }
    private void TurnSoundOn()
    {
        foreach (Sound s in sounds)
        {
            if(!s.native)
            s.source.mute = false;
        }
    }

    private void TurnSoundOff()
    {
        foreach (Sound s in sounds)
        {
            if (!s.native)
                s.source.mute = true;
        }
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
        saveAndLoadPlayerData.SaveSequence();
    }

    public void ApplySoundBool()
    {
        if (soundBool.value)
        {
            TurnSoundOn();
        }
        else
        {
            TurnSoundOff();
        }
    }
}
