using System.Reflection;
using UnityEngine;
using System;

public class carteaccesInteraction : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool doSomething;

    private bool canGoOffline = false;
    private bool outlineIsOn = false;

    public GameObject gameManager;
        private gererAccesZones gererAccesZones;
    public string zoneAssocier;

    private PropertyInfo propertyInfo;
    private FieldInfo fieldInfo;
    private Type type;


    private void Start()
    {
        gameManager = GameObject.Find("gameManager");

        gererAccesZones = gameManager.GetComponent<gererAccesZones>();

        type = gererAccesZones.GetType();

        // Recuperer ces champs
        propertyInfo = type.GetProperty(zoneAssocier);
        fieldInfo = type.GetField(zoneAssocier);
    }


    // Update is called once per frame
    void Update()
    {




        if (gameObject.GetComponent<objetProximity>().actif)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;

            if (!outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = true;


                outlineIsOn = true;
            }


            // Interaction avec objet faite
            if (gameObject.GetComponent<objetProximity>().doSomething)
            {
                Debug.Log("Je " + (this.gameObject.name) + " fait quelque chose");

                // L'OBJET FAIT QUELQUE CHOSE
                    
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(gererAccesZones, true);
                    Debug.Log("Valeur de " + zoneAssocier + " (propertyInfo) : " + propertyInfo.GetValue(gererAccesZones));
                    }   
                    else if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(gererAccesZones, true);

                        Debug.Log("Valeur de " + zoneAssocier + " (fieldInfo) : " + fieldInfo.GetValue(gererAccesZones));
                    }
                    else
                    {
                        Debug.Log("Erreur : " + zoneAssocier + " n'existe pas");
                    }


                    // Apres avoir pick up la carte, la détruire
                    Destroy(this.gameObject);

                // 


                gameObject.GetComponent<objetProximity>().doSomething = false;
            }




            canGoOffline = true;
        }
        else if (canGoOffline)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;

            if (outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = false;
                outlineIsOn = false;
            }




            canGoOffline = false;
        }
    }
}
