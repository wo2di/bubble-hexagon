using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject[] objsToDisable;
    public void DisableGameobject()
    {
        foreach(GameObject obj in objsToDisable)
        {
            obj.SetActive(false);
        }
        
    }

}
