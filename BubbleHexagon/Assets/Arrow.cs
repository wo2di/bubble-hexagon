using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public RayCaster rayCaster;

    public void SetArrowDirection()
    {
        gameObject.SetActive(true);
        transform.rotation = Quaternion.identity;
        float theta = Vector2.SignedAngle(Vector2.up, rayCaster.direction);
        transform.Rotate(new Vector3(0,0,theta));
    }

    public void ResetArrow()
    {
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

}
