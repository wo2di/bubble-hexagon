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
    public Collider2D fireZone;

    public BubbleParent bubbleParent;
    /// 
    public List<Collider2D> colls1;
    public List<Collider2D> colls2;

    public BoolSO gamePaused;

    public void ClampAndSetDirection()
    {
        Vector3 v = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - fireTR.position).normalized;
        float theta = Vector2.SignedAngle(Vector2.right, v);
        theta = Mathf.Clamp(theta, thetaMin, thetaMax);
        direction = new Vector2(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad));
    }
    public void GetSlotByRay(out Slot target, out List<Vector3> waypoints)
    {
        target = null;
        waypoints = new List<Vector3>();

        // 일시정지상태인지 확인
        if (gamePaused.value) return;

        // 클릭한 곳이 영역 내부에 있는지 확인
        RaycastHit2D[] zoneTest = Physics2D.GetRayIntersectionAll(new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward));
        var x = from h in zoneTest
                     where h.collider == fireZone
                     select h;
        if (x.Count() == 0) return;

        //direction 값 계산
        ClampAndSetDirection();


        RaycastHit2D[] hits = Physics2D.CircleCastAll(fireTR.position, rayRadius, direction, 50, layerMask);

        RaycastHit2D remove = hits.ToList().Find(h => h.collider.gameObject == bubbleParent.bubble1.gameObject);
        if(remove.collider != null)
        {
            List<RaycastHit2D> removed = hits.ToList();
            removed.Remove(remove);
            hits = removed.ToArray();
        }

        ///
        var result = from h in hits
                select h.collider;
        colls1 = result.ToList();
        colls2.Clear();


        RaycastHit2D hit;
        Collider2D coll;
        if (hits.Length >= 1)
        {
            hit = hits[0];
            coll = hit.collider;

            if (coll.GetComponent<Wall>() != null)
            {
                waypoints.Add(hit.centroid);

                hits = Physics2D.CircleCastAll(hit.centroid, rayRadius, -hit.point, 50, layerMask);

                ///
                var result2 = from h in hits
                              //where h.collider.gameObject.GetComponent<Wall>() == null
                              select h.collider;
                colls2 = result2.ToList();

                hit = hits[1];
                coll = hit.collider;
            }

            if(coll.transform.IsChildOf(bubbleParent.transform))
            {
                Direction direction = GetSlotDirectionByHit(hit);
                target = coll.GetComponent<Bubble>().slot.GetPairByDir(direction).slot;
                waypoints.Add(hit.centroid);
            }    
            //if (coll.transform.parent == bubbleParent.transform)
            //{
                
            //}
        }

        //debug
        for(int i = 0; i < waypoints.Count -1; i++)
        {
            Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red, 1f);
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
