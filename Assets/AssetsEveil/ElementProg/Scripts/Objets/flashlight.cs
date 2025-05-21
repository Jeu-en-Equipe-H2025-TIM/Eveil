using System;
using System.Collections;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    // Valeur pour la flashlight
    private Light lumiere;
    private float tempsAttente;
    private float qteFlickers;

    private void OnEnable()
    {
        // On active la flashlight
        Debug.Log("Flashlight activée");
        lumiere = GetComponent<Light>();

        Invoke("determinerFlashlightVariables", 0f);
        StartCoroutine("FlashlightFlicker");
    }

    private void OnDisable()
    {
        StopCoroutine("FlashlightFlicker");
    }



    void controleFlashlight()
    {
        // Si on, turn off. Si off, turn on (avec setActive)
        if (lumiere.isActiveAndEnabled)
        {
            lumiere.enabled = false;
        }
        else
        {

            this.GetComponent<AudioSource>().Play();
            lumiere.enabled = true;

        }

    }

    void determinerFlashlightVariables()
    {
        tempsAttente = UnityEngine.Random.Range(1f, 10f);
        qteFlickers = UnityEngine.Random.Range(1f, 3f);

        Debug.Log("Flashlight: Nouvelles valeurs: " + tempsAttente + "s, " + qteFlickers + " flickers. ");
    }

    IEnumerator FlashlightFlicker()
    {
        Debug.Log("Flicker lancer!");
        yield return new WaitForSeconds(tempsAttente);
        Invoke("determinerFlashlightVariables", 0f);

        // Active le flicker 
        yield return StartCoroutine("FlashlightFlickerAction", qteFlickers);

        Debug.Log("Flicker en cours, on continue");
        if (lumiere.isActiveAndEnabled)
        {
            Debug.Log("On relance le flickering");
            yield return FlashlightFlicker();
        }
        else
        {
            Debug.Log("On arrete le flickering");
            yield return default;
        }
    }

    IEnumerator FlashlightFlickerAction(float qteFlickers)
    {
        Debug.Log("Flicker Action lancer!");
        // Flicker X fois
        for (int i = 0; i < qteFlickers; i++)
        {
            Debug.Log("Flicker " + i);
            lumiere.enabled = false;
            yield return new WaitForSeconds(0.1f);
            lumiere.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Flicker Action fini!");
        yield return default;
    }
}