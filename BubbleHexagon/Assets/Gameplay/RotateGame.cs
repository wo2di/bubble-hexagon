using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGame : MonoBehaviour
{
    public float speed = 10;

    public void RotateToResult(Quaternion result)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, result, Time.deltaTime * speed) ;
    }
}
