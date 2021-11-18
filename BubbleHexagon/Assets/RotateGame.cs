using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGame : MonoBehaviour
{
    float speed = 50;

    public void RotateToResult(Quaternion result)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, result, Time.deltaTime * speed) ;
    }
}
