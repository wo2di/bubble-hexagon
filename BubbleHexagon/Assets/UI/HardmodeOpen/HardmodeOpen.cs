using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HardmodeOpen : MonoBehaviour
{
    public Sound bgm;
    public BoolSO gamePaused;
    //public BoolSO musicOn;
    public MuteSound musicButton;
    public Button greyPanel;
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
        musicButton.ApplySoundBool();
        //bgm.TurnOn();
    }

    public void CanDisable()
    {
        greyPanel.interactable = true;
        //canDisable = true;
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButtonUp(0) && canDisable)
    //    {
    //        GetComponent<Disabler>().DisableGameobjects();
    //    }
    //}
}
