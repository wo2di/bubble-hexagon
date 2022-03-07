using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWithSound : MonoBehaviour
{
    public string soundName;

    private void OnEnable()
    {
        Debug.Log("enabled");
        FindObjectOfType<AudioManager>().PlaySound(soundName);
    }

}
