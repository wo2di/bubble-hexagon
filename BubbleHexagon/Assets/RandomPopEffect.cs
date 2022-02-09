using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPopEffect : MonoBehaviour
{
    public GameEvent itemApplied;
    public void EndOfEffect()
    {
        itemApplied.Raise();
        Destroy(gameObject);
    }
}
