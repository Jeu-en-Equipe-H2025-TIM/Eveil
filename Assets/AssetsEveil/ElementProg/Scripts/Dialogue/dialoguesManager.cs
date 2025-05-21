using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class dialoguesManager : MonoBehaviour
{


    // Liste des dialogues "lineaires" (1 time, avec l'histoire)
    public List<String> dialoguesLineaire = new List<String>();

    // Liste des dialogues d'aide (quand le joueur est pris dans une zone pendant X de temps)
    public List<String> dialoguesAide = new List<String>();

    // Liste des dialogues random (1 time, called randomly)
    public List<String> dialogueRandom = new List<String>();

    private String dialogueActif;

    // Boite de dialogue
    public GameObject dialogueBox;

    // Temps apparition/disparition des dialogues
    public float durationFondu;

   


    public void dialogueTrigger(String liste, int index, float duree, AudioClip audio) // Liste dans laquelle on pige, o� on pige, et dur�e du dialogue (temps sur l'�cran)
    {
        switch (liste)
        {
            case "lineaire":
                dialogueActif = dialoguesLineaire[index];
                break;

            case "aide":
                dialogueActif = dialoguesAide[index];
                break;

            case "random":
                // On choisit un nombre au hasard entre 0 et la taille de la liste
                int random = UnityEngine.Random.Range(0, dialogueRandom.Count);
                dialogueActif = dialogueRandom[random];
                break;

            default:
                Debug.Log("Erreur : la liste de dialogue n'existe pas");
                return;
        }

        // On affiche le texte de la liste de dialogues pertinente
        dialogueBox.GetComponent<TextMeshProUGUI>().text = dialogueActif;

        // Audio
        this.GetComponent<AudioSource>().PlayOneShot(audio);

        StartCoroutine(dialogueApparait());
        Invoke("finDialogue", duree);

    }


    public void finDialogue()
    {
        Debug.Log("DIALOGUE: Temps �coul�, on enleve le dialogue");

        StartCoroutine(dialogueDisparait());
    }

      public void arretSon()
    {
         this.GetComponent<AudioSource>().Stop();
    }


    private IEnumerator dialogueApparait()
    {
        // Nos variables
        float tempsPasser = 0;
        Color couleur = dialogueBox.GetComponent<TextMeshProUGUI>().color;

        // �a n'apparait que quand d�j� invisible (d�j� � 0), mais au cas o�
        couleur.a = 0;

        // Moins en moins visible
        while (tempsPasser < durationFondu)
        {
            couleur.a = Mathf.Clamp01(tempsPasser / durationFondu);
            dialogueBox.GetComponent<TextMeshProUGUI>().color = couleur;
            tempsPasser += Time.deltaTime;
            yield return null; // Aka attend la prochaine frame et non un temps X 
        }

        // � la fin, peu importe, on le met visible 100% (1)
        couleur.a = 1;
        dialogueBox.GetComponent<TextMeshProUGUI>().color = couleur;
    }

    private IEnumerator dialogueDisparait()
    {
        // Nos variables
        float tempsPasser = 0;
        Color couleur = dialogueBox.GetComponent<TextMeshProUGUI>().color;

        // �a ne disparait que quand visible, aka d�j� � 1, mais on le hardset juste au cas o�
        couleur.a = 1;

        // Moins en moins visible
        while (tempsPasser < durationFondu)
        {
            couleur.a = Mathf.Clamp01(1 - (tempsPasser / durationFondu));
            dialogueBox.GetComponent<TextMeshProUGUI>().color = couleur;
            tempsPasser += Time.deltaTime;
            yield return null; // Aka attend la prochaine frame et non un temps X 
        }

        // � la fin, peu importe, on la remet invisible
        couleur.a = 0;
        dialogueBox.GetComponent<TextMeshProUGUI>().color = couleur;
        // Et on enl�ve le texte au cas o�
        dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
    }

}