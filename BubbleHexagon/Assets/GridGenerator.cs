using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GridGenerator : MonoBehaviour
{
    public Transform poolTr;
    public Transform gridParent;
    public int depth;

    public Queue<GameObject> pool;
    public Slot[] slots;

    //Generates Hexagon Grid, Use in Inspector
    [ContextMenu("Generate Grid")]
    public void GenerateSlotsAll()
    {
        for (int i = 0; i < depth; i++)
        {
            pool = new Queue<GameObject>();
            foreach(Slot s in GetComponentsInChildren<Slot>())
            {
                pool.Enqueue(s.gameObject);
            }
            slots = gridParent.GetComponentsInChildren<Slot>();

            foreach (Slot s in slots)
            {
                GenerateSlots(s);
            }
        }

        slots = gridParent.GetComponentsInChildren<Slot>();
        foreach (Slot s in slots)
        {
            EditorUtility.SetDirty(s);
            PrefabUtility.RecordPrefabInstancePropertyModifications(s);
        }
    }


    public void GenerateSlots(Slot s)
    {
        foreach(DirSlotPair p in s.adjacents)
        {
            if(p.slot == null)
            {
                GameObject obj = pool.Dequeue();
                Slot slot = obj.GetComponent<Slot>();
                obj.transform.SetParent(gridParent);
                slot.level = s.level + 1;

                
                float degree = ((int)p.direction) * 60 + 30;
                float radian = degree * Mathf.Deg2Rad;
                Debug.Log(degree);
                int i = (int) ((degree + 150) % 360) / 60;
                obj.transform.SetPositionAndRotation((new Vector3(Mathf.Cos(radian), Mathf.Sin(radian))) + s.transform.position, Quaternion.identity);

                p.slot = slot;
                slot.GetPairByDir((Direction)i).slot = s;

            }
        }

        for(int i = 0; i<6; i++)
        {
            s.GetPairByDir((Direction)i).slot.GetPairByDir((Direction)((i + 2) % 6)).slot = s.GetPairByDir((Direction)((i + 1) % 6)).slot;
            s.GetPairByDir((Direction)i).slot.GetPairByDir((Direction)((i + 4) % 6)).slot = s.GetPairByDir((Direction)((i + 5) % 6)).slot;
        }
    }
}
