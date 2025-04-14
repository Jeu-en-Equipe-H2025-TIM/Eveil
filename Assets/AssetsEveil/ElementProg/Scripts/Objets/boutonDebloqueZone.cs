using System.Reflection;
using System;
using UnityEngine;

public class boutonDebloqueZone : MonoBehaviour
{
    // Variables generales de detection du joueur et si le joueur intéragit avec l'objet
    private bool proximiteJoueur;
    private bool doSomething;

    // Variables propres au bouton
    private bool canGoOffline = false;
    private bool outlineIsOn = false;
    private bool estActif = false;

    // Si le bouton necessite un access pour l'utilisation
    public bool estBloquer;
    public string debloqueAssocier;
    private System.Object statusDebloqueAssocierTemp;
    private bool statusDebloqueAssocier;

    // Le bouton ouvre X porte
    public GameObject gameManager;
    private gererAccesZones gererAccesZones;
    public string zoneAssocier;
    private System.Object statusZoneAssocierTemp;
    private bool statusZoneAssocier;

    private PropertyInfo propertyInfo;
    private FieldInfo fieldInfo;
    private Type type;


    private void Start()
    {
        gameManager = GameObject.Find("gameManager");
        gererAccesZones = gameManager.GetComponent<gererAccesZones>();

        type = gererAccesZones.GetType();
    }

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

                if (estBloquer)
                {
                    if (checkSiDebloquer())
                    {
                        Invoke("interactionBouton", 0f);
                    }

                }
                else
                {
                    Invoke("interactionBouton", 0f);
                }



                gameObject.GetComponent<objetProximity>().doSomething = false;
            }




            canGoOffline = true;
        }
        else if (canGoOffline)
        {

            if (outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = false;
                outlineIsOn = false;
            }


            canGoOffline = false;
        }
    }

    public bool checkSiDebloquer()
    {
        // Recuperer ces champs
        propertyInfo = type.GetProperty(debloqueAssocier);
        fieldInfo = type.GetField(debloqueAssocier);

        // Recuperer le status de la valeur zone associer de gameObjet
        if (propertyInfo != null)
        {
            statusDebloqueAssocierTemp = propertyInfo.GetValue(gererAccesZones);
        }
        else if (fieldInfo != null)
        {
            statusDebloqueAssocierTemp = fieldInfo.GetValue(gererAccesZones);
        }
        else
        {
            Debug.Log("Erreur : " + debloqueAssocier + " n'existe pas");
        }

        // Transforme le System.objet en boolean (pour les ifs plus bas)
        statusDebloqueAssocier = Convert.ToBoolean(statusDebloqueAssocierTemp);

        return statusDebloqueAssocier;
    }

    public void interactionBouton() // Bouton a été activé
    {

        Debug.Log("Je " + (this.gameObject.name) + " fait quelque chose");
        propertyInfo = type.GetProperty(zoneAssocier);
        fieldInfo = type.GetField(zoneAssocier);

            this.GetComponent<MeshRenderer>().material.color = Color.yellow;


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

        // Attendre une seconde, et ensuite fermer le bouton
        Invoke("fermerBouton", 1f);

        gameObject.GetComponent<objetProximity>().doSomething = false;
    }

    public void fermerBouton() // Ferme le bouton
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;


        if (propertyInfo != null)
        {
            propertyInfo.SetValue(gererAccesZones, false);
            Debug.Log("Valeur de " + zoneAssocier + " (propertyInfo) : " + propertyInfo.GetValue(gererAccesZones));
        }
        else if (fieldInfo != null)
        {
            fieldInfo.SetValue(gererAccesZones, false);

            Debug.Log("Valeur de " + zoneAssocier + " (fieldInfo) : " + fieldInfo.GetValue(gererAccesZones));
        }
        else
        {
            Debug.Log("Erreur : " + zoneAssocier + " n'existe pas");
        }
    }
}
