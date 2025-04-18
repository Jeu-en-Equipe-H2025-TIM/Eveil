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
     * 1: Hallucinations quand on pick-up un objet "trigger" qui va lancer une hallucination (masque, upgrade, etc.) (qui s'arrête après X de temps)
     * 2: Entrer dans une zone qui va trigger une hallucination (qui s'arrête après X de temps) 
     * 3: Entrer dans une zone qui va avoir une hallucination (qui s'arrête après X de temps APRÈS avoir quitter la zone) 
     *
     * Composants en commun: 
     * - "Lancer hallucination"
     * - "Arrêter hallucination"
     * 2 et 3: OnTriggerEnter
     * 
     * Composants uniques: 
     * 1: Besoin d'une fonction public qui peut être called par des objets extérieurs / par un script qui n'est pas celui-ci
     * 3: Besoin d'un OnTriggerExit pour lancer le timer pour arrêter l'hallucination
     */

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject hallucinationGroupe;
    [SerializeField] private GameObject hallucinationUI;
    private bool statusHallucination = false;

    void Start()
    {

        // Ces deux affectations ne marchent pas
        canvas = GameObject.Find("Canvas");
        hallucinationGroupe = GameObject.Find("HallucinationCanvas");
        hallucinationUI = GameObject.Find("Hallucination");

    }

    /* 
    * 1: Hallucinations quand on pick-up un objet "trigger" qui va lancer une hallucination (masque, upgrade, etc.) (qui s'arrête après X de temps)
    */
    public void gererHallucination(float duree)
    {
        Invoke("lancerHallucination", 0f); // On lance l'hallucination immédiatement
        StartCoroutine(FinHallucination(duree)); // On lance la coroutine pour arrêter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "zoneHallucination")
        {
            Debug.Log("On entre dans une zone d'hallucinations");
            if (other.gameObject.GetComponent<zonesHallucinations>().zoneTrigger) // Si la zone est une zone trigger ou non
            {
                /* 
                * 2: Entrer dans une zone qui va trigger une hallucination (qui s'arrête après X de temps) 
                */
                Invoke("lancerHallucination", 0f); // On lance l'hallucination 
                StartCoroutine(FinHallucination(other.gameObject.GetComponent<zonesHallucinations>().dureeHallucination));  // On lance la coroutine pour arrêter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
            }
            else
            {
                /* 
                * 3: Entrer dans une zone qui va trigger une hallucination (qui s'arrête après X de temps APRÈS avoir quitter la zone) 
                */
                Invoke("lancerHallucination", 0f); // On lance l'hallucination 

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        /* 
        * 3: Entrer dans une zone qui va avoir une hallucination (qui s'arrête après X de temps APRÈS avoir quitter la zone) 
        */
        if (other.gameObject.tag == "zoneHallucination")
        {
            if (!other.gameObject.GetComponent<zonesHallucinations>().zoneTrigger) // Si la zone n'est pas une zone trigger
            {
                StartCoroutine(FinHallucination(other.gameObject.GetComponent<zonesHallucinations>().dureeHallucination)); // On lance la coroutine pour arrêter l'hallucination et on lui donne la qte de temps qu'il attend avant de finir
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

        // Réduire l'opacité de l'élément UI

        while (hallucinationUI.GetComponent<Image>().color.a > 0 && !statusHallucination)
        {
            Debug.Log("FinHallucination: On diminue l'opacité");
            Debug.Log("Opacité actuelle: =" + hallucinationUI.GetComponent<Image>().color.a + " / 255"); // Debug pour voir l'opacité actuelle de l'élément UI
            // Changer l'opacité de l'élément UI 
            Color color = hallucinationUI.GetComponent<Image>().color;
            color.a -= 0.01f;
            hallucinationUI.GetComponent<Image>().color = color;
            yield return null; // On attend une frame
        }
        // HALLUCINATION ARRÊTE
    }

    private IEnumerator ProgressionHallucination()
    {

        statusHallucination = true;


        // Augmenter l'opacité de l'élément UI

        // On change l'opacité de ces éléments (enfants de hallucinationGroupe)
        while (hallucinationUI.GetComponent<Image>().color.a < 0.2 && statusHallucination)
        {
            Debug.Log("ProgressionHallucination: On augmente l'opacité");
            Debug.Log("Opacité actuelle =" + hallucinationUI.GetComponent<Image>().color.a + " / 255");
            // Changer l'opacité de l'élément UI 
            Color color = hallucinationUI.GetComponent<Image>().color;
            color.a += 0.01f;
            hallucinationUI.GetComponent<Image>().color = color;
            yield return null; // On attend une frame
        }

    }
}
