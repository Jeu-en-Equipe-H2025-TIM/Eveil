using System.Reflection;
using System;
using UnityEngine;

public class porteBesoinSignal : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool canGoOffline = false;


    public GameObject gameManager;
    private gererAccesZones gererAccesZones;
    public string zoneAssocier;
    private System.Object statusZoneAssocierTemp;
    private bool statusZoneAssocier;

    private bool porteDisponible = true;
    private bool porteStatus = false;

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
        proximiteJoueur = gameObject.GetComponent<objetProximity>().actif;

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


        // Si le joueur approche + la zone est d�bloqu�e
        if (statusZoneAssocier)
        {
            // Porte Ouvre
            if (porteDisponible && !porteStatus)
            {
                Invoke("ouvrirPorte", 0f);
            }

        }
        else
        {
            // Porte ferme
            if (porteDisponible && porteStatus)
            {
                Invoke("fermerPorte", 0f);
            }
        }
    }

    public void ouvrirPorte()
    {
        porteDisponible = false;
        Invoke("rendrePorteDisponible", 1f);
        

        // mettre animation ouverture
        this.GetComponent<Animator>().SetTrigger("OuvrirDoor");

        canGoOffline = true;
    }

    public void fermerPorte()
    {
        porteDisponible = false;
        Invoke("rendrePorteDisponible", 1f);
        

        // mettre animation fermeture
        this.GetComponent<Animator>().SetTrigger("FermerDoor");

        canGoOffline = false;
    }

    public void rendrePorteDisponible()
    {
        porteDisponible = true;
        porteStatus = !porteStatus;
    }
}
