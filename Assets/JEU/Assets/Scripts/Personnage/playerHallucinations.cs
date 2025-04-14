using Mono.Cecil.Cil;
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


    [SerializeField] private GameObject hallucinationGroupe;

    void Start()
    {
        hallucinationGroupe = GameObject.Find("HallucinationCanvas");
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
                StartCoroutine(FinHallucination(other.gameObject.GetComponent<zonesHallucinations>().dureeHallucination));  // On lance la coroutine pour arr�ter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
            }
            else
            {
                /* 
                * 3: Entrer dans une zone qui va trigger une hallucination (qui s'arr�te apr�s X de temps APR�S avoir quitter la zone) 
                */
                Invoke("lancerHallucination", 0f); // On lance l'hallucination 

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


        // HALLUCINATION ARR�TE
        // Progressif? @melanie donc pour l'instant: instant
        hallucinationGroupe.SetActive(false); // On d�sactive le canvas d'hallucination
    }

    private IEnumerator ProgressionHallucination()
    {

        // hallucinationGroupe est un gameObject vide qui contient les �l�ments UIs (comme des images)
        hallucinationGroupe.SetActive(true);
    
        // On change l'opacit� de ces �l�ments (enfants de hallucinationGroupe)
        while (hallucinationGroupe.GetComponentInChildren<Image>().color.a < 0.5)
        {
            hallucinationGroupe.GetComponentInChildren<Image>().color.a = 0.5;
            yield return null; // On attend une frame
        }

        yield return new WaitForSeconds(0.01f);
    }
}
