using UnityEngine;
using System;

public class uiCredits : MonoBehaviour
{
    public GameObject arik;
    public GameObject melanie;
    public GameObject emile;
    public GameObject wichardson;


    public void montreArik()
    {
        melanie.SetActive(false);
        wichardson.SetActive(false);
        emile.SetActive(false);
        arik.SetActive(true);
    }

    public void montreMelanie()
    {
        melanie.SetActive(true);
        wichardson.SetActive(false);
        emile.SetActive(false);
        arik.SetActive(false);
    }

    public void montreEmile()
    {
        melanie.SetActive(false);
        wichardson.SetActive(false);
        emile.SetActive(true);
        arik.SetActive(false);
    }

    public void montreWichardson()
    {
        melanie.SetActive(false);
        wichardson.SetActive(true);
        emile.SetActive(false);
        arik.SetActive(false);
    }
}
