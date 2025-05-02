using System;
using System.Collections;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    // Valeur pour les upgrades
    public bool upgradeDebloquer = false;

    // Valeur pour la flashlight
    public GameObject flashlight;

    // Update is called once per frame

    private void Start()
    {
    }
    void Update()
    {
        // Flashlight
        if (upgradeDebloquer && Input.GetKeyDown(KeyCode.F))
        {
            Invoke("controleFlashlight", 0f);
        }
        //

        if (upgradeDebloquer && Input.GetMouseButtonDown(0))
        {
            Invoke("attaque", 0f);
        }
    }


    void controleFlashlight()
    {
        // Si on, turn off. Si off, turn on (avec setActive)
        if (flashlight.activeSelf)
        {
            flashlight.SetActive(false);
        }
        else
        {
            flashlight.SetActive(true);

        }

    }

    void attaque()
    {
        // Lancer l'attaque dans son script
        this.GetComponent<playerAttaque>().attaque();
    }

}