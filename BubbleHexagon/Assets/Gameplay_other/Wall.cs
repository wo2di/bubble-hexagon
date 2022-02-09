using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    EdgeCollider2D edgeCollider;

    private void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        SetSize(4.45f);
    }

    public void SetSize(float i)
    {
        float y = (1f / 3f) + (i / 2f);
        float x = y * Mathf.Sqrt(3);
        float ratio = (x - (1f / Mathf.Sqrt(3) - 1f / 2f)) / x;
        x *= ratio;
        y *= ratio;

        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(-x, -y));
        points.Add(new Vector2(-x, y));
        points.Add(new Vector2(0, 2 * y));
        points.Add(new Vector2(x, y));
        points.Add(new Vector2(x, -y));
        edgeCollider.SetPoints(points);
    }

}
