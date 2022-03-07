using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrajectory : MonoBehaviour
{
    public LineRenderer line;
    public Transform shootTR;


    void Start()
    {
        ResetTrajectory();
    }

    public void DrawTrajectory(List<Vector3> waypoints)
    {
        line.positionCount = waypoints.Count + 1;

        for(int i = 0; i<waypoints.Count; i++)
        {
            line.SetPosition(i + 1, waypoints[i]);
        }
    }

    public void ResetTrajectory()
    {
        line.positionCount = 1;
        line.SetPosition(0, shootTR.position);
    }
}
