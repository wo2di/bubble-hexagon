using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RayCaster : MonoBehaviour
{
    public float rayRadius = 0.4f;
    public Transform fireTR;
    public LayerMask layerMask;

    public Vector3 direction;

    public float thetaMin = 30f;
    public float thetaMax = 150f;

    /// 
    public List<Collider2D> colls1;
    public List<Collider2D> colls2;

    public void ClampAndSetDirection()
    {
        Vector3 v = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - fireTR.position).normalized;
        float theta = Vector2.SignedAngle(Vector2.right, v);
        theta = Mathf.Clamp(theta, thetaMin, thetaMax);
        direction = new Vector2(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad));
    }
    public void GetSlotByRay(out Slot target, out List<Vector3> waypoints)
    {
        ClampAndSetDirection();

        target = null;
        waypoints = new List<Vector3>();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(fireTR.position, rayRadius, direction, 50, layerMask);
        
        ///
        var result = from h in hits
                select h.collider;
        colls1 = result.ToList();
        colls2.Clear();

        RaycastHit2D hit;
        Collider2D coll;
        if (hits.Length >= 2)
        {
            hit = hits[1];
            coll = hit.collider;

            if (coll.GetComponent<Wall>() != null)
            {
                waypoints.Add(hit.centroid);

                hits = Physics2D.CircleCastAll(hit.point, rayRadius, -hit.point, 50, layerMask);

                ///
                var result2 = from h in hits
                             select h.collider;
                colls2 = result2.ToList();

                hit = hits[1];
                coll = hit.collider;
            }

            Direction direction =  GetSlotDirectionByHit(hit);
            target = coll.GetComponent<Bubble>().slot.GetPairByDir(direction).slot;
            waypoints.Add(hit.centroid);
        }
    }


    public Direction GetSlotDirectionByHit(RaycastHit2D hit)
    {
        Vector3 hitpoint = hit.collider.transform.parent.InverseTransformPoint(hit.point);
        Vector3 bubblePosition = hit.collider.transform.localPosition;
        float theta = Vector2.SignedAngle(Vector2.right, ((Vector2)(hitpoint - bubblePosition)).normalized);

        if (-120 <= theta && theta < -60)
        {
            return Direction.dir5;
        }
        else if (theta < -120)
        {
            return Direction.dir4;
        }
        else if (120 <= theta)
        {
            return Direction.dir3;
        }
        else if (60 <= theta && theta < 120)
        {
            return Direction.dir2;
        }
        else if (0 <= theta && theta < 60)
        {
            return Direction.dir1;
        }
        else
        {
            return Direction.dir6;
        }
    }
}
