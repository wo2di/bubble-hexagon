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

    [HideInInspector]
    public AudioSource source;

    public void SetSource()
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
    }
}
