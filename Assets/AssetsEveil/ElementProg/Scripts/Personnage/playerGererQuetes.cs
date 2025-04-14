using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class playerGererQuetes : MonoBehaviour
{

    public GameObject questsManager;

    public bool passeALaQueteSuivante = false;

    [SerializeField] private string queteActuelle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questsManager = GameObject.Find("questsManager");
        queteActuelle = questsManager.GetComponent<questsManager>().listeQuetes[0];
    }

    // Update is called once per frame
    void Update()
    {
        // On reaffiche quelle est la quete actuelle et la suivante (Dev mode voir si tout marche bien)
        queteActuelle = questsManager.GetComponent<questsManager>().listeQuetes[0];
        //
    }
}
