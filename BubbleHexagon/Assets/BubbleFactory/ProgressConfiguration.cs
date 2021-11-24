using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProgressConfiguration : ScriptableObject
{
    [System.Serializable]
    public class BubbleProbability 
    {
        public string bubble;
        [Range(0,1)]
        public float probability;
    }

    [Range(1, 9)]
    public int colorIndex;
    public List<BubbleProbability> bubbleProbabilities;

    public void SetBubbleProbability(string bubble, float probability)
    {
        bubbleProbabilities.Find(p => p.bubble == bubble).probability = probability;
    }

    public void SetColorIndex(int i)
    {
        colorIndex = i;
    }

    public string GetBubbleByProbability()
    {
        float random = Random.value;
        float f = 0;
        foreach(BubbleProbability p in bubbleProbabilities)
        {
            if (f < random && random < f + p.probability) return p.bubble;
            f = f + p.probability;
        }
        return "color";
    }
}
