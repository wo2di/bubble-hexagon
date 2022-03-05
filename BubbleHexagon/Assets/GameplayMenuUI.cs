using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenuUI : MonoBehaviour
{
    public bool isExpanded;

    public GameObject[] buttons;
    public SoundButton[] soundButtons;

    public GameObject pauseAndResume;
    public GameObject grayPanel;

    public Sprite pauseSprite;
    public Sprite resumeSprite;

    //public BoolSO gamePaused;

    private void EnableButtons()
    {
        foreach(GameObject obj in buttons)
        {
            obj.SetActive(true);
        }
    }

    private void DisableButtons()
    {
        foreach (GameObject obj in buttons)
        {
            obj.SetActive(false);
        }
    }

    public void Expand()
    {
        EnableButtons();
        grayPanel.SetActive(true);
        isExpanded = true;
        pauseAndResume.GetComponent<Image>().sprite = resumeSprite;
    }

    public void Collapse()
    {
        DisableButtons();
        grayPanel.SetActive(false);
        isExpanded = false;
        pauseAndResume.GetComponent<Image>().sprite = pauseSprite;
    }

    public void OnPauseClick()
    {
        if (!isExpanded)
        {
            Expand();
        }
        else
        {
            Collapse();
        }
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
