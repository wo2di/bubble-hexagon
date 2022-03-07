using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardmodeOpen : MonoBehaviour
{
    public Sound bgm;
    public BoolSO gamePaused;
    bool canDisable;
    private void OnEnable()
    {
        gamePaused.value = true;
        bgm.TurnOff();
    }
    private void OnDisable()
    {
        gamePaused.value = false;
        canDisable = false;
        bgm.TurnOn();
    }

    public void CanDisable()
    {
        canDisable = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && canDisable)
        {
            GetComponent<Disabler>().DisableGameobjects();
        }
    }
}
