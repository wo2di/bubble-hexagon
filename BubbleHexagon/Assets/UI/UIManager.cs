using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTopText;
    public IntegerSO score;
    public IntegerSO scoreTop;

    private void Start()
    {
        scoreTopText.text = "TOP " + scoreTop.value.ToString();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = score.value.ToString();
        if(score.value > scoreTop.value)
        {
            scoreTopText.text = "NEW BEST";
            scoreText.color = Color.red;
        }
    }
}
