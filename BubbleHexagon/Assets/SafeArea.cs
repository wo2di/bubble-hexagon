using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rect_tr;
    public Rect safe_area;
    public Resolution resolution;

    public void UpdateSafeArea()
    {
        Vector2 anchor_min = new Vector2();
        Vector2 anchor_max = new Vector2();

        anchor_min.x = 0;
        anchor_max.x = 1;
        anchor_min.y = safe_area.yMin / resolution.height;
        anchor_max.y = safe_area.yMax / resolution.height;

        rect_tr.anchorMin = anchor_min;
        rect_tr.anchorMax = anchor_max;
    }

    private void Awake()
    {
        rect_tr = GetComponent<RectTransform>();
        safe_area = Screen.safeArea;
        resolution = Screen.currentResolution;
    }

    void Start()
    {
#if UNITY_EDITOR
#else
        UpdateSafeArea();
#endif
    }

}