using System.Reflection;
using UnityEngine;
using System;

public class objetInteraction : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool doSomething;


    private bool canGoOffline = false;
    private bool outlineIsOn = false;

    public GameObject gameManager;
        private gererAccesZones gererAccesZones;

    public bool estAssocierAUneZone;
    public string zoneAssocier;

    // Quetes
    public GameObject questsManager;
    public bool estAssocierAUneQuete;
    public int delaisTriggerQueteEnSecondes;
    [SerializeField] private string queteAssociee;
    public bool relierAUneHallucination;

    private PropertyInfo propertyInfo;
    private FieldInfo fieldInfo;
    private Type type;

    // Dialogues
    public GameObject dialogueManager;
    public Boolean dialogueLineaire;
    public Boolean dialogueAide;
    public Boolean dialogueRandom;
    private String dialogueListeAssociee;
    public int positionDuTexteDansLaListe;
    public float dureeSurEcran;
    public bool dialogueDeHallucination;


    private void Start()
    {
        dialogueManager = GameObject.Find("dialoguesManager");
        questsManager = GameObject.Find("questsManager");
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
        // Update la quete associer si associer � une quete (sinon il y aura erreur car undefined/null)
        if (estAssocierAUneQuete)
        {
            queteAssociee = this.GetComponent<queteAssocier>().queteAssocieeNom;
        }


       
        if (gameObject.GetComponent<objetProximity>().actif)
        {
            // Effet visuel de la proximite du joueur
                // this.GetComponent<MeshRenderer>().material.color = Color.green;
                if (!outlineIsOn)
                {
                    this.GetComponent<Outline>().enabled = true;


                    outlineIsOn = true;
                }
            //


            // Interaction avec objet faite
            if (gameObject.GetComponent<objetProximity>().doSomething)
            {
                Debug.Log("Je " + (this.gameObject.name) + " fait quelque chose");

                // L'OBJET FAIT QUELQUE CHOSE
                    
                // ASSOCIER A UNE ZONE!
                if (estAssocierAUneZone)
                {
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
                }

                // ASSOCIER A UNE QUETE
                if (estAssocierAUneQuete)
                {
                    
                    if (questsManager.GetComponent<questsManager>().listeQuetes[0] == queteAssociee)
                    {
                        questsManager.GetComponent<questsManager>().queteTrigger(delaisTriggerQueteEnSecondes, relierAUneHallucination);
                    }
                    else
                    {
                        Debug.Log("Le joueur a skip une quete..?");
                        Debug.Log("ZONE: Quete associee = " + queteAssociee);
                        Debug.Log("ZONE: Quete [0] (actuelle) du manager = " + questsManager.GetComponent<questsManager>().listeQuetes[0]);
                    }
                }


                // ASSOCIER A UN DIALOGUE 

                if (dialogueLineaire || dialogueAide || dialogueRandom)
                {

                    if (dialogueLineaire)
                    {
                        dialogueListeAssociee = "lineaire";
                    }
                    else if (dialogueAide)
                    {
                        dialogueListeAssociee = "aide";
                    }
                    else if (dialogueRandom)
                    {
                        dialogueListeAssociee = "random";
                    }
                    else
                    {
                        Debug.Log("Je: " + this.gameObject.name + " n'a pas de liste de dialogues associée! Tu as oublié de checkmark l'une des trois options!");
                    }

                    dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger(dialogueListeAssociee, positionDuTexteDansLaListe, dureeSurEcran, dialogueDeHallucination);
                }

                // Apres avoir pick up la carte, la d�truire
                Destroy(this.gameObject);
                // 


                gameObject.GetComponent<objetProximity>().doSomething = false;
            }
            //




            canGoOffline = true;
        }
        else if (canGoOffline)
        {
            // this.GetComponent<MeshRenderer>().material.color = Color.red;

            if (outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = false;
                outlineIsOn = false;
            }




            canGoOffline = false;
        }
    }
}
