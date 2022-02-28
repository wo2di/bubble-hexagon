using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject gameObject;
    public void DisableGameobject()
    {
        gameObject.SetActive(false);
    }

}
