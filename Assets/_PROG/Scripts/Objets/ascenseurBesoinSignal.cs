using System.Reflection;
using System;
using UnityEngine;
using System.Collections;

public class ascenseurBesoinSignal : MonoBehaviour
{
    private bool canGoOffline = false;


    public GameObject gameManager;
    private gererAccesZones gererAccesZones;
    public string zoneAssocier;
    private System.Object statusZoneAssocierTemp;
    private bool statusZoneAssocier;

    private PropertyInfo propertyInfo;
    private FieldInfo fieldInfo;
    private Type type;

    private bool enMouvement = false;
    private float hauteurActuelle;
    public float hauteurHaut;
    public float hauteurBas;
    public float vitesseDeMouvement;

    public GameObject joueur;
    private bool proximiteJoueur;

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

        proximiteJoueur = gameObject.GetComponent<objetProximity>().actif;
        hauteurActuelle = this.gameObject.transform.position.y;

        // Recuperer le status de la valeur zone associer de gameObjet
        if (propertyInfo != null)
        {
            statusZoneAssocierTemp = propertyInfo.GetValue(gererAccesZones);
        }
        else if (fieldInfo != null)
        {
            statusZoneAssocierTemp = fieldInfo.GetValue(gererAccesZones);
        }
        else
        {
            Debug.Log("Erreur : " + zoneAssocier + " n'existe pas");
        }

        // Transforme le System.objet en boolean (pour les ifs plus bas)
        statusZoneAssocier = Convert.ToBoolean(statusZoneAssocierTemp);


        // Si pas actuellement utiliser
        if (!enMouvement)
        {
            // Si debloquer (levier activé)
            if (statusZoneAssocier)
            {
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                if (hauteurActuelle >= hauteurHaut)
                {
                    enMouvement = true;
                    Debug.Log("En haut!");
                    StartCoroutine(goDown());
                } else // Else pas en utilisation + 100% en bas
                {
                    enMouvement = true;
                    Debug.Log("En bas!");
                    StartCoroutine(goUp());
                }


                canGoOffline = true;

            }
            else
            {
                // Do nothing
                this.GetComponent<MeshRenderer>().material.color = Color.red;

                canGoOffline = false;
            }
        }



    }


    private IEnumerator goUp()
    {
        if (hauteurActuelle < hauteurHaut)
        {
            enMouvement = true;
            while (hauteurActuelle < hauteurHaut)
            {
                //hauteurActuelle = Mathf.Lerp(hauteurActuelle, hauteurHaut, Time.deltaTime * vitesseDeMouvement);
                hauteurActuelle += vitesseDeMouvement * Time.deltaTime;
                this.transform.position = new Vector3(transform.position.x, hauteurActuelle, transform.position.z);
                if (proximiteJoueur) // On deplace AUSSI le joueur s'il est sur la plateforme
                {
                    joueur.transform.position = new Vector3(joueur.transform.position.x, joueur.transform.position.y + vitesseDeMouvement * Time.deltaTime, joueur.transform.position.z);
                }

                Debug.Log("Hauteur actuelle: " + this.transform.position.y);
                Debug.Log("Hauteur voulu" + hauteurHaut);
                yield return null;
            }
            enMouvement = false;
        }
    }

    private IEnumerator goDown()
    {
        if (hauteurActuelle > hauteurBas)
        {
            enMouvement = true;
            while (hauteurActuelle > hauteurBas)
            {
                //hauteurActuelle = Mathf.Lerp(hauteurActuelle, hauteurBas, Time.deltaTime * vitesseDeMouvement);
                hauteurActuelle -= vitesseDeMouvement * Time.deltaTime;
                this.transform.position = new Vector3(transform.position.x, hauteurActuelle, transform.position.z);
                if (proximiteJoueur) // On deplace AUSSI le joueur s'il est sur la plateforme
                {
                    joueur.transform.position = new Vector3(joueur.transform.position.x, joueur.transform.position.y - vitesseDeMouvement * Time.deltaTime, joueur.transform.position.z);
                }

                Debug.Log("Hauteur actuelle: " + this.transform.position.y);
                Debug.Log("Hauteur voulu" + hauteurHaut);
                yield return null;
            }
            enMouvement = false;
        }

    }
}
