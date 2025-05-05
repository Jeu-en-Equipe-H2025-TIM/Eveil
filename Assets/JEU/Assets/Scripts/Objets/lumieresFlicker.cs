using System;
using System.Collections;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class lumieresFlicker : MonoBehaviour
{
    public GameObject versionActive;
    public GameObject versionInactive;
    private Light lumiere;
    private float tempsAttente;
    private float qteFlickers;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lumiere = GetComponent<Light>();


        Invoke("determinerLumiereVariables", 0f);
        StartCoroutine("LumiereFlicker");
    }

    // Update is called once per frame


    void determinerLumiereVariables()
    {
        tempsAttente = UnityEngine.Random.Range(1f, 10f);
        qteFlickers = UnityEngine.Random.Range(1f, 3f);

    }

    IEnumerator LumiereFlicker()
    {
        Debug.Log("Flicker lancer!");
        yield return new WaitForSeconds(tempsAttente);
        Invoke("determinerLumiereVariables", 0f);

        // Active le flicker 
        yield return StartCoroutine("LumiereFlickerAction", qteFlickers);

        if (lumiere.isActiveAndEnabled)
        {
            yield return LumiereFlicker();
        }
        else
        {
            yield return default;
        }
    }

    IEnumerator LumiereFlickerAction(float qteFlickers)
    {
        // Flicker X fois (sequence)
        for (int i = 0; i < qteFlickers; i++)
        {
            lumiere.enabled = false;
            versionActive.SetActive(false);
            versionInactive.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lumiere.enabled = true;
            versionActive.SetActive(true);
            versionInactive.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        //Debug.Log("Sequence de flicker fini!");
        yield return default;
    }
}
