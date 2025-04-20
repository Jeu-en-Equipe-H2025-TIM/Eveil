using System;
using System.Collections.Generic;
using UnityEngine;

public class puzzle1Manager : MonoBehaviour
{



    // Variables des objets exterieurs
    public GameObject gameManager;

    public GameObject bouton1;
    public GameObject bouton2;
    public GameObject bouton3;
    public GameObject bouton4;

    public GameObject lumiere1;
    public GameObject lumiere2;
    public GameObject lumiere3;
    public GameObject lumiere4;

    // Variables aux calculs

    public int stack;
    List<int> ordreDesBoutons = new List<int> { 1, 2, 3, 4 };

    // Variable status
    public bool statusPuzzle; // False = fini



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gameManager = GameObject.Find("gameManager");
        statusPuzzle = true;
        stack = 0;

        Debug.Log("Ordre actuel: " + string.Join(",", ordreDesBoutons));
        melangeOrdre(ordreDesBoutons);
        Debug.Log("Ordre apres melange: " + string.Join(",", ordreDesBoutons));
        melangeOrdre(ordreDesBoutons);
        Debug.Log("Apres un AUTRE melange: " + string.Join(",", ordreDesBoutons));

        appliquerValeursAuxBoutons(ordreDesBoutons);
        Debug.Log("Bouton 1 = " + bouton1 + "| Bouton 2 = " + bouton2 + "| Bouton 3 = " + bouton3 + "| Bouton 4 = " + bouton4);


    }

    // Update is called once per frame
    void Update()
    {
        if (stack == 4)
        {
            Debug.Log("Stack complet, on a appuyer sur les boutons dans le bon ordre!");
            gameManager.GetComponent<gererAccesZones>().puzzle1Fin = true;

            statusPuzzle = false;
            stack++; // Pour ne pas re-rentrer dans la boucle (et on ne re-utilise pas le puzzle donc meh!
        }
    }

    void melangeOrdre<T> (List<T> ordre)
    {
        for (int i = 0; i < ordre.Count - 1; i++)
        {
            int valeurAleatoire = UnityEngine.Random.Range(0, ordre.Count);
            T valeurTemporaire = ordre[i];
            ordre[i] = ordre[valeurAleatoire];
            ordre[valeurAleatoire] = valeurTemporaire;

        }
    }

    void appliquerValeursAuxBoutons(List<int> ordre)
    {
        bouton1.GetComponent<boutonPuzzle1>().valeur = ordre[0];
        bouton2.GetComponent<boutonPuzzle1>().valeur = ordre[1];
        bouton3.GetComponent<boutonPuzzle1>().valeur = ordre[2];
        bouton4.GetComponent<boutonPuzzle1>().valeur = ordre[3];
    }


}
