using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.source = source;
            sound.SetSource();
        }
    }

    private void Start()
    {
        PlaySound("bgm");
    }

    public void PlaySound(string name)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == name);
        if (sound != null)
        {
            sound.source.Play();
        }
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
