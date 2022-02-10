using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public IntegerSO score;
    
    public void UpdateScoreUI()
    {
        scoreText.text = score.value.ToString();
    }
}
