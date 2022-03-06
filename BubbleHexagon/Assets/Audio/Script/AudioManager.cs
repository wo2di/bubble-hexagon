using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool playBGM;

    private void Awake()
    {
        AndroidNativeAudio.makePool();
        foreach (Sound sound in sounds)
        {
            if(sound.native)
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
        if (sound != null)
        {
            if(sound.native)
            {
                sound.SoundID = AndroidNativeAudio.play(sound.FileID);
            }
            else
            {
                sound.source.Play();
            }
            //StartCoroutine(PlaySoundCoroutine(sound.source, sound.clip));
            
        }
    }

    public IEnumerator PlaySoundCoroutine(AudioSource source , AudioClip clip)
    {
        yield return null;
        //source.Play();
        source.PlayOneShot(clip);
    }


    public void TurnOffBGM()
    {
        Sound sound = System.Array.Find(sounds, s => s.name == "bgm");
        sound.source.mute = true;
    }

    public IEnumerator RepeatSoundWithDelay(string name, int repeat, float delay)
    {
        for (int i = 0; i < repeat; i++)
        {
            PlaySound(name);
            yield return new WaitForSeconds(delay);
        }
    }

}
