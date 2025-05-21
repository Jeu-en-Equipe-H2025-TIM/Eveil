// using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;

public class playerHallucinations : MonoBehaviour
{
    /* Hallucinations
     * 1: Hallucinations quand on pick-up un objet "trigger" qui va lancer une hallucination (masque, upgrade, etc.) (qui s'arr�te apr�s X de temps)
     * 2: Entrer dans une zone qui va trigger une hallucination (qui s'arr�te apr�s X de temps) 
     * 3: Entrer dans une zone qui va avoir une hallucination (qui s'arr�te apr�s X de temps APR�S avoir quitter la zone) 
     *
     * Composants en commun: 
     * - "Lancer hallucination"
     * - "Arr�ter hallucination"
     * 2 et 3: OnTriggerEnter
     * 
     * Composants uniques: 
     * 1: Besoin d'une fonction public qui peut �tre called par des objets ext�rieurs / par un script qui n'est pas celui-ci
     * 3: Besoin d'un OnTriggerExit pour lancer le timer pour arr�ter l'hallucination
     */

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject hallucinationGroupe;
    [SerializeField] private GameObject hallucinationUI;
    private bool statusHallucination = false;



    // Dealer avec les zones d'hallucinations qui joue avec les qu�tes

    public GameObject questManager;
    public int delaisTriggerQueteEnSecondes;
    [SerializeField] private string queteAssociee;

    void Start()
    {

        questManager = GameObject.Find("questsManager");

        // Ces deux affectations ne marchent pas
        canvas = GameObject.Find("Canvas");
        hallucinationGroupe = GameObject.Find("HallucinationCanvas");
        hallucinationUI = GameObject.Find("Hallucination");

    }

    /* 
    * 1: Hallucinations quand on pick-up un objet "trigger" qui va lancer une hallucination (masque, upgrade, etc.) (qui s'arr�te apr�s X de temps)
    */
    public void gererHallucination(float duree)
    {
        Invoke("lancerHallucination", 0f); // On lance l'hallucination imm�diatement
        StartCoroutine(FinHallucination(duree)); // On lance la coroutine pour arr�ter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "zoneHallucination")
        {
            Debug.Log("On entre dans une zone d'hallucinations");
            if (other.gameObject.GetComponent<zonesHallucinations>().zoneTrigger) // Si la zone est une zone trigger ou non
            {
                /* 
                * 2: Entrer dans une zone qui va trigger une hallucination (qui s'arr�te apr�s X de temps) 
                */
                Invoke("lancerHallucination", 0f); // On lance l'hallucination 

                // On update la quete (si zone d'hallucinations est associ�e)
                if (other.gameObject.GetComponent<zonesHallucinations>().triggerQuete)
                {
                    queteAssociee = other.gameObject.GetComponent<zonesHallucinations>().queteAssociee;
                    Invoke("updateQuete", other.gameObject.GetComponent<zonesHallucinations>().triggerQueteDelais);
                }

                StartCoroutine(FinHallucination(other.gameObject.GetComponent<zonesHallucinations>().dureeHallucination));  // On lance la coroutine pour arr�ter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
            }
            else
            {
                /* 
                * 3: Entrer dans une zone qui va trigger une hallucination (qui s'arr�te apr�s X de temps APR�S avoir quitter la zone) 
                */
                Invoke("lancerHallucination", 0f); // On lance l'hallucination 

                // On update la quete (si zone d'hallucinations est associ�e)
                if (other.gameObject.GetComponent<zonesHallucinations>().triggerQuete)
                {
                    queteAssociee = other.gameObject.GetComponent<zonesHallucinations>().queteAssociee;
                    Invoke("updateQuete", other.gameObject.GetComponent<zonesHallucinations>().triggerQueteDelais);
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        /* 
        * 3: Entrer dans une zone qui va avoir une hallucination (qui s'arr�te apr�s X de temps APR�S avoir quitter la zone) 
        */
        if (other.gameObject.tag == "zoneHallucination")
        {
            if (!other.gameObject.GetComponent<zonesHallucinations>().zoneTrigger) // Si la zone n'est pas une zone trigger
            {
                StartCoroutine(FinHallucination(other.gameObject.GetComponent<zonesHallucinations>().dureeHallucination)); // On lance la coroutine pour arr�ter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
            }
        }
    }

    private void lancerHallucination()
    {
        Debug.Log("Hallucination commence");
        // ENDROIT POUR L'AUDIO

        // HALLUCINATION FAIT QUELQUE CHOSE
        // Progressif? @melanie donc pour l'instant: instant
        //hallucinationGroupe.SetActive(true); // On active le canvas d'hallucination

        // Si progressif: 
        StartCoroutine("ProgressionHallucination");
    }

    private IEnumerator FinHallucination(float dureeAvantArret)
    {
        yield return new WaitForSeconds(dureeAvantArret);

        Debug.Log("Hallucination prend fin");

        statusHallucination = false;

        // R�duire l'opacit� de l'�l�ment UI

        while (hallucinationUI.GetComponent<Image>().color.a > 0 && !statusHallucination)
        {
            Debug.Log("FinHallucination: On diminue l'opacit�");
            Debug.Log("Opacit� actuelle: =" + hallucinationUI.GetComponent<Image>().color.a + " / 255"); // Debug pour voir l'opacit� actuelle de l'�l�ment UI
            // Changer l'opacit� de l'�l�ment UI 
            Color color = hallucinationUI.GetComponent<Image>().color;
            color.a -= 0.01f;
            hallucinationUI.GetComponent<Image>().color = color;
            yield return null; // On attend une frame
        }
        // HALLUCINATION ARR�TE
    }

    private IEnumerator ProgressionHallucination()
    {

        statusHallucination = true;


        // Augmenter l'opacit� de l'�l�ment UI

        // On change l'opacit� de ces �l�ments (enfants de hallucinationGroupe)
        while (hallucinationUI.GetComponent<Image>().color.a < 0.1 && statusHallucination)
        {
            Debug.Log("ProgressionHallucination: On augmente l'opacit�");
            Debug.Log("Opacit� actuelle =" + hallucinationUI.GetComponent<Image>().color.a + " / 255");
            // Changer l'opacit� de l'�l�ment UI 
            Color color = hallucinationUI.GetComponent<Image>().color;
            color.a += 0.01f;
            hallucinationUI.GetComponent<Image>().color = color;
            yield return null; // On attend une frame
        }

    }


    private void updateQuete()
    {

        if (questManager.GetComponent<questsManager>().listeQuetes[0] == queteAssociee)
        {
            questManager.GetComponent<questsManager>().queteTrigger(0); // 0 car on gere deja le delais dans le onTriggerEnter
        }
        else
        {
            Debug.Log("Le joueur a skip une quete..?");
            Debug.Log("ZONE: Quete associee = " + queteAssociee);
            Debug.Log("ZONE: Quete [0] (actuelle) du manager = " + questManager.GetComponent<questsManager>().listeQuetes[0]);
        }
    }
}