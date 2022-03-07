using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoManager : MonoBehaviour
{
    public GameObject[] howtos;
    public GameObject openPage;
    public int openPagenum;
    public GameObject[] Arrows;

    public void NextPage()
    {
        DisableAll();
        SetOpenPage(openPagenum + 1);
        SetArrow();
    }

    public void PreviousPage()
    {
        DisableAll();
        SetOpenPage(openPagenum - 1);
        SetArrow();
    }
    private void DisableAll()
    {
        foreach(GameObject obj in howtos)
        {
            obj.SetActive(false);
        }
    }

    private void SetOpenPage(int i)
    {
        howtos[i].SetActive(true);
        openPage = howtos[i];
        openPagenum = i;
    }

    private void SetArrow()
    {
        foreach(GameObject obj in Arrows)
        {
            obj.SetActive(true);
        }
        if(openPagenum == 0)
        {
            Arrows[0].SetActive(false);
        }
        if(openPagenum == howtos.Length-1)
        {
            Arrows[1].SetActive(false);
        }
    }
}
