using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoManager : MonoBehaviour
{
    public GameObject[] howtos;
    public GameObject openPage;
    public int openPagenum;
    public GameObject[] Arrows;

    private void OnEnable()
    {
        SetOpenPage(0);
    }

    public void NextPage()
    {
        SetOpenPage(openPagenum + 1);
    }

    public void PreviousPage()
    {
        SetOpenPage(openPagenum - 1);
    }

    private void SetOpenPage(int i)
    {
        DisableAll();

        howtos[i].SetActive(true);
        openPage = howtos[i];
        openPagenum = i;

        SetArrow();

    }
    private void DisableAll()
    {
        foreach(GameObject obj in howtos)
        {
            obj.SetActive(false);
        }
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
