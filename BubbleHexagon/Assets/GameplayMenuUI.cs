using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenuUI : MonoBehaviour
{
    public bool isExpanded;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject grayPanel;

    public Sprite pause;
    public Sprite collapse;
    public Sprite title;

    public BoolSO gamePaused;


    private void Start()
    {
        button3.GetComponent<SoundButton>().ApplySoundBool();
        button4.GetComponent<SoundButton>().ApplySoundBool();
    }
    public void Expand()
    {
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);
        grayPanel.SetActive(true);
        isExpanded = true;
        button1.GetComponent<Image>().sprite = collapse;
    }

    public void Collapse()
    {
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        grayPanel.SetActive(false);
        isExpanded = false;
        button1.GetComponent<Image>().sprite = pause;
    }

    public void OnPauseClick()
    {
        if (!isExpanded)
        {
            Expand();
            gamePaused.value = true;
        }
        else
        {
            Collapse();
            gamePaused.value = false;
        }
    }

    public void GoToTitle()
    {
        gamePaused.value = false;
        SceneManager.LoadScene("TitleScene");
    }

}
