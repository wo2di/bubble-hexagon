using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatisticsDisplayer : MonoBehaviour
{
    public GameDifficultySO difficultySO;
    public RectTransform[] modes;

    public IntegerSO[] topScores;
    public Statistics statistics;
    public FloatSO[] totalPlaytimes;

    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI playtimeText;

    private void OnEnable()
    {
        DisplayStatistics((int)difficultySO.value);
    }

    public void DisplayStatistics(int i)
    {
        modes[i].sizeDelta = new Vector2(280, 90);
        modes[(i + 1) % 2].sizeDelta = new Vector2(210, 90);


        topScoreText.text = "Top Score\n" + topScores[i].value.ToString();
        texts[0].text = statistics.topShootCnts[i].value.ToString();
        texts[1].text = statistics.topPopCnts[i].value.ToString();
        texts[2].text = statistics.topDropCnts[i].value.ToString();
        texts[3].text = statistics.totalShootCnts[i].value.ToString();
        texts[4].text = statistics.totalPopCnts[i].value.ToString();
        texts[5].text = statistics.totalDropCnts[i].value.ToString();
        playtimeText.text = "Total Playtime\n" + (totalPlaytimes[i].value / 3600).ToString("F1") + " hours";
    }

}
