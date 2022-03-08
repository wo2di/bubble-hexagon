using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWithSound : MonoBehaviour
{
    public string soundName;

    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().PlaySound(soundName);
    }

}
