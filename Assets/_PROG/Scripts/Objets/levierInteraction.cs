using JetBrains.Annotations;
using System.Reflection;
using UnityEngine;
using System;

public class levierInteraction : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool doSomething;


    private bool canGoOffline = false;
    private bool outlineIsOn = false;
    private bool estActif = false;


    // Update is called once per frame
    void Update()
    {




        if (gameObject.GetComponent<objetProximity>().actif)
        {

            if (!outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = true;


                outlineIsOn = true;
            }

            if (gameObject.GetComponent<objetProximity>().doSomething)
            {
                Debug.Log("Je " + (this.gameObject.name) + " fait quelque chose");

                // Levier = RESTE on apres utilisation
                // Levier est deja actif. On le "ferme"
                if (estActif)
                {
                    estActif = false;

                    // L'OBJET FAIT QUELQUE CHOSE
                    this.GetComponent<MeshRenderer>().material.color = Color.red;


                    // Levier est inactif. On l'active
                }
                else
                {
                    estActif = true;

                    // L'OBJET FAIT QUELQUE CHOSE
                    this.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }


                gameObject.GetComponent<objetProximity>().doSomething = false;
            }




            canGoOffline = true;
        }
        else if (canGoOffline)
        {
            Debug.Log("Je " + (this.gameObject.name) + " suis offline");

            if (outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = false;
                outlineIsOn = false;
            }




            canGoOffline = false;
        }
    }
}
