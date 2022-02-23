using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneInitialize : MonoBehaviour
{
    public SaveAndLoadPlayerData saveAndLoad;

    void Start()
    {
        saveAndLoad.LoadSequence();
    }

}
