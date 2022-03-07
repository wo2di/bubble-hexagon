using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool playBGM;

    private void Awake()
    {
        Debug.Log("awake of audio manager");
        AndroidNativeAudio.makePool();
        if(Application.platform == RuntimePlatform.Android)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.native)
                {
                    sound.FileID = AndroidNativeAudio.load(sound.nativePath);
                }
                else
                {
                    AudioSource source = gameObject.AddComponent<AudioSource>();
                    sound.source = source;
                    sound.SetSource();
                }
            }
        }
        else
        {
            foreach (Sound sound in sounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                sound.source = source;
                sound.SetSource();
            }
        }

    }

    private void Start()
    {
        if(playBGM)
        {
            PlaySound("bgm");
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == name);
        if(sound != null)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (sound.native)
                {
                    sound.SoundID = AndroidNativeAudio.play(sound.FileID, sound.nativeVolume);
                }
                else
                {
                    sound.source.Play();
                }
            }
            else
            {
                sound.source.Play();
            }
        }
    }


    public void TurnOffBGM()
    {
        Sound sound = System.Array.Find(sounds, s => s.name == "bgm");
        sound.source.mute = true;
    }

}
