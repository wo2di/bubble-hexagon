using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenuUI : MonoBehaviour
{
    public GameConfigurationSO gameConfig;
    public bool isExpanded;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject grayPanel;

    public Sprite pause;
    public Sprite collapse;
    public Sprite title;
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public Sound music;
    public List<Sound> soundEffects;

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
        Debug.Log("!!");
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

    public void TurnMusicOn()
    {
        music.source.mute = false;
    }

    public void TurnMusicOff()
    {
        music.source.mute = true;
    }

    public void TurnSoundOn()
    {
        foreach(Sound s in soundEffects)
        {
            s.source.mute = false;
        }
    }

    public void TurnSoundOff()
    {
        foreach (Sound s in soundEffects)
        {
            s.source.mute = true;
        }
    }

    public void OnMusicButton()
    {
        Image image = button4.GetComponent<Image>();
        if(gameConfig.musicOn)
        {
            TurnMusicOff();
            gameConfig.musicOn = false;
            image.sprite = musicOff;
        }
        else
        {
            TurnMusicOn();
            gameConfig.musicOn = true;
            image.sprite = musicOn;
        }
    
    }

    public void OnSoundButton()
    {
        Image image = button3.GetComponent<Image>();
        if (gameConfig.soundOn)
        {
            TurnSoundOff();
            gameConfig.soundOn = false;
            image.sprite = soundOff;
        }
        else
        {
            TurnSoundOn();
            gameConfig.soundOn = true;
            image.sprite = soundOn;
        }
    }
}
