using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameplayUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTopText;
    public IntegerSO score;
    public IntegerSO scoreTop;
    public Color newBestColor;

    public BoolSO isFirstPlay;
    public GameObject howto;
    public SaveAndLoadPlayerData saveAndLoadPlayerData;

    private void Start()
    {
        scoreTopText.text = "TOP " + scoreTop.value.ToString();
        if(isFirstPlay.value)
        {
            howto.SetActive(true);
            isFirstPlay.value = false;
            saveAndLoadPlayerData.SaveSequence();
        }
    }

    public void UpdateScoreUI()
    {
        scoreText.text = score.value.ToString();
    }

    public void TopScore()
    {
        scoreTopText.text = "NEW BEST";
        scoreTopText.color = newBestColor;
    }
}
