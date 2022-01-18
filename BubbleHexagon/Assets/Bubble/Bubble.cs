using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Slot slot;

    public void SetSlot(Slot s)
    {
        slot = s;
        slot.bubble = this;
    }

    public void UnSlot()
    {
        slot.bubble = null;
        slot = null;
    }

    public void FitToSlot()
    {
        transform.position = slot.transform.position;
    }

    public void Delete()
    {
        UnSlot();
        Destroy(gameObject);
    }

    public void Pop()
    {
        UnSlot();
        Destroy(gameObject);
    }

    public void Drop()
    {
        UnSlot();
        gameObject.layer = 2;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3;
        Vector2 v = new Vector2(Random.value - 0.5f, 1);
        rb.AddForce(v * 300);

    }


    public IEnumerator TranslateToSlot()
    {
        Vector3 to = slot.transform.position;
        while(Vector3.Distance(transform.position, to) > 0.01)
        {
            transform.position = Vector3.Lerp(transform.position, to, 0.3f);
            yield return null;
        }
            transform.position = to;

    }

    //public float speed = 20;
    //public bool translated = false;

    //public IEnumerator Translate(Vector3[] waypoints)
    //{
    //    foreach(Vector3 v in waypoints)
    //    {
    //        while(Vector3.Distance(transform.position, v) > 0.1)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, v, speed * Time.deltaTime);
    //            yield return null;
    //        }
    //    }
    //    translated = true;
    //}
}
