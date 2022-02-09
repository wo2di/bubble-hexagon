using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfigurationSO : ScriptableObject
{
    public Difficulty difficulty;

    public bool musicOn;
    public bool soundOn;
}