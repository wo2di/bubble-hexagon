using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedAdButton : MonoBehaviour
{
    public AdTest adTest;
    private void OnMouseUpAsButton()
    {
        adTest.ShowAd();
    }

}
