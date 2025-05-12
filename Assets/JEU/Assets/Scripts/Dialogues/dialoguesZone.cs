using System;
using System.Collections;
using UnityEngine;

public class dialoguesZone : MonoBehaviour
{
    // Variables
    public GameObject dialogueManager;
    // Texte
    public Boolean dialogueLineaire;
    public Boolean dialogueAide;
    public Boolean dialogueRandom;
    private String dialogueListeAssociee;
    public int positionDuTexteDansLaListe;

    // Settings du texte
    public float delaisAvantDialogue;
    public float dureeSurEcran;
    public bool estUneHallucination;

    private Boolean joueurDansZone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // On choisi la liste selon la boolean sélectionnée (par dev)
        dialogueManager = GameObject.Find("dialoguesManager");
        if (dialogueLineaire)
        {
            dialogueListeAssociee = "lineaire";
        } else if (dialogueAide)
        {
            dialogueListeAssociee = "aide";
        } else if (dialogueRandom)
        {
            dialogueListeAssociee = "random";
        } else
        {
            Debug.Log("Je: " + this.gameObject.name + " n'a pas de liste de dialogues associée! Tu as oublié de checkmark l'une des trois options!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Si c'est le joueur, on check si c'est un dialogue random ou non
        if (other.gameObject.tag == "Player")
        {
            joueurDansZone = true;
            if (dialogueListeAssociee != "random")
            {
                // On refresh dialogueManager aka reset ces fonctions aka on ne garde pas le timer de duree sur ecran 
                dialogueManager.SetActive(false);
                dialogueManager.SetActive(true);
                Invoke("activerDialogue", delaisAvantDialogue); // On attend un delais avant de lancer le dialogue
            }
            else
            { // Dialogue random = un délais random avant de le mettre
                StartCoroutine(boucleDialogueRandom());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quand on quitte, on arrête le dialogue 
        if (other.gameObject.tag == "Player")
        {
            joueurDansZone = false;
            if (dialogueListeAssociee == "lineaire")
            {
                // Lineaire
                Destroy(this.gameObject);
            }
            else if (dialogueListeAssociee == "random")
            { // On arrete le dialogue random
                StopCoroutine(boucleDialogueRandom());
            }

        }
    }

    private IEnumerator boucleDialogueRandom()
    {
        // Timer random pour le dialogue random
        int timer = UnityEngine.Random.Range(0, 10);
        yield return new WaitForSeconds(timer);

        if (joueurDansZone)
        {
            dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger(dialogueListeAssociee, positionDuTexteDansLaListe, dureeSurEcran, estUneHallucination);
            // Boucle infinie tant qu'on ne l'arrete pas manuellement
            StartCoroutine(boucleDialogueRandom());
        }

    }

    private void activerDialogue()
    {
        // Appeler la fonction dans dialogueManager
        if (joueurDansZone)
        {
            dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger(dialogueListeAssociee, positionDuTexteDansLaListe, dureeSurEcran, estUneHallucination);
        }
    }

}
