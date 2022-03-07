using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public new string name;
    public AudioClip clip;
    
    [Range(0, 1)]
    public float volume;
    public bool loop;

    public bool native;
    public string nativePath;
    public int FileID;
    public int SoundID;
    public float nativeVolume;

    [HideInInspector]
    public AudioSource source;

    public void SetSource()
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
    }

    public void TurnOn()
    {
        if (source != null)
        {
            source.mute = false;
        }
        else
        {
            nativeVolume = volume;
        }
    }

    public void TurnOff()
    {
        if (source != null)
        {
            source.mute = true;
        }
        else
        {
            nativeVolume = 0;
        }
    }


}
