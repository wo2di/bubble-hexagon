using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    public Statistics statistics;
    public GameDifficultySO gameConfig;
    public StringSO adStatus;

    private void OnGUI()
    {
        string time = "Seconds: " + statistics.time.ToString();
        string content1 = "Difficulty: " + gameConfig.difficulty.ToString();
        string content2 = "ShootCount: " + statistics.shootCnt.value.ToString();
        string content3 = "PopCount: " + statistics.popCnt.value.ToString();
        string content4 = "DropCount: " + statistics.dropCnt.value.ToString();
        string content5 = "AdStatus: " + adStatus.value;

        GUILayout.Label("");
        GUILayout.Label("");
        GUILayout.Label($"<color='black'><size=40>{time}</size></color>");
        GUILayout.Label($"<color='black'><size=40>{content1}</size></color>");
        GUILayout.Label($"<color='black'><size=40>{content2}</size></color>");
        GUILayout.Label($"<color='black'><size=40>{content3}</size></color>");
        GUILayout.Label($"<color='black'><size=40>{content4}</size></color>");
        GUILayout.Label($"<color='black'><size=40>{content5}</size></color>");
    }
}
