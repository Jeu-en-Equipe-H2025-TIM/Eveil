using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menus : MonoBehaviour
{

    // Variables pour gerer le fondu au noir
    private float durationFondu = 1f;
    public GameObject ecranNoir;
    public Image imageNoire;

    // Variables des differents canvas
    public GameObject canvas;
    public GameObject canvasIntro;
    public GameObject canvasJeu;
    public GameObject canvasPause;
    public GameObject canvasFin;
    public GameObject livreMenu;
    public GameObject canvasCredits;
    public GameObject canvasReglages;

    private string ancienneScene;


    // Variables generales au script
    private Boolean fin = false; // Empecher de spammer / faire une partie du script en boucle



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(fonduAuVisible());

    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "MenuPrincipal") {
            livreMenu = GameObject.Find("BinderComplet");
            }

        // Ouverture du menu pause 
        if (Application.isPlaying)
        {
            // Mettre pause (menu in-game)
            if (Input.GetKeyDown(KeyCode.M))
            {
                Invoke("pause", 0f);
            }
        }
    }


    //
    // FONCTIONS DES BOUTONS QUI FONT CHANGER LES SCENES / UIs
    // 1: Commencez (� partir du menu principal seulement)
    // 2: Reglages (� partir du menu principal + in-game)
    // 3: Cr�dits (� partir du menu principal seulement)
    // 4: Menu principal (� partir du in-game, credits, reglages)
    // 5: Quitter (� partir du menu principal seulement)

    public void commencez()
    {
        ancienneScene = SceneManager.GetActiveScene().name;
        Debug.Log("Bouton UI: Commencer le jeu");
        // Animation du livre du ferme 
        // ICI
        livreMenu.GetComponent<Animator>().SetTrigger("Commencer");

        // GetComponent<AudioSource>().PlayOneShot(sonUI);

        // ACTION - Fera 1s de fondu au noir et ensuite change la scene automatiquement
        StartCoroutine(fonduChangerSceneDefondu("Jeu"));

    }

    public void accueil()
    {
        Debug.Log("Bouton UI: Accueil (menu principal)");

        Debug.Log("Scene actuelle: "+SceneManager.GetActiveScene().name);

        // GetComponent<AudioSource>().PlayOneShot(sonUI);

        // ACTION - Fera 1s de fondu au noir et ensuite change la scene automatiquement
        if (SceneManager.GetActiveScene().name == "Jeu")
        {
            Debug.Log("Accueil version en jeu");
            Invoke("pause", 0f);

            // Deux facons qu'on peut quitter le jeu: Reglages et Menu principal
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(fonduChangerSceneDefondu("MenuPrincipal"));
        }
        else
        {
            Debug.Log("Accueil version hors-jeu");
            StartCoroutine(fonduChangerSceneDefondu(ancienneScene));
        }
        ancienneScene = SceneManager.GetActiveScene().name;
    }

    public void credits()
    {
        ancienneScene = SceneManager.GetActiveScene().name;
        Debug.Log("Bouton UI: Credits");

        // GetComponent<AudioSource>().PlayOneShot(sonUI);

        // Animation du livre du ferme 
        // ICI
        livreMenu.GetComponent<Animator>().SetTrigger("Credit");

        // ACTION - Fera 1s de fondu au noir et ensuite change la scene automatiquement
        StartCoroutine(fonduChangerSceneDefondu("Credits"));
    }

    public void reglages()
    {
        ancienneScene = SceneManager.GetActiveScene().name;
        Debug.Log("Bouton UI: Reglages");
        Debug.Log("Scene actuelle: " + SceneManager.GetActiveScene().name);

        // Deux facons qu'on peut quitter le jeu: Reglages et Menu principal
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // GetComponent<AudioSource>().PlayOneShot(sonUI);

        if (SceneManager.GetActiveScene().name == "MenuPrincipal")
        {
            // Animation du livre du ferme 
            // ICI
            livreMenu.GetComponent<Animator>().SetTrigger("Reglages");

            // ACTION - Fera 1s de fondu au noir et ensuite change la scene automatiquement
            StartCoroutine(fonduChangerSceneDefondu("Reglages"));
        }
        else
        {
            // ACTION - Fera 1s de fondu au noir et ensuite change la scene automatiquement
            Invoke("pause", 0f);
            StartCoroutine(fonduChangerSceneDefondu("Reglages"));
        }

    }

    public void quit()
    {
        ancienneScene = SceneManager.GetActiveScene().name;
        Debug.Log("Bouton UI: Quitter le jeu (fermer le jeu)");
        // GetComponent<AudioSource>().PlayOneShot(sonUI);


        // Animation du livre du ferme 
        // ICI
        livreMenu.GetComponent<Animator>().SetTrigger("Quitter");

        Application.Quit(); 



    }



    public void pause()
    {
        // GetComponent<AudioSource>().PlayOneShot(sonUI);

        Debug.Log("PAUSE TRIGGERED");
        // Si le jeu est en pause, on le remet en marche
        if (Time.timeScale == 0)
        {
            Debug.Log("On quitte la pause");
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            canvasPause.SetActive(false);
        }
        // Sinon, on le met en pause
        else
        {
            Debug.Log("On va en pause");
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            this.GetComponent<gameManager>().sauvegardePositionJoueur();

            canvasPause.SetActive(true);
            //
        }
    }



    //
    // CHANGEMENT DE SCENE (PARENT FONDU CHANGER SCENE DE FONDU) + 2 DEPENDANCES (FONDU AU NOIR ET FONDU AU VISIBLE)
    //

    private IEnumerator fonduChangerSceneDefondu(string nomDeScene)
    {
        //GetComponent<AudioSource>().PlayOneShot(sonUI);
        yield return StartCoroutine(fonduAuNoir());


        switch (nomDeScene)
        {
            case ("Jeu"):
                // A partir du menu principal: 1 animation

                canvasIntro.SetActive(false);
                canvasJeu.SetActive(true);
                canvasFin.SetActive(false);
                canvasReglages.SetActive(false);

                break;

            case ("Credits"):
                // A partir du menu principal: 2 animations
                canvasCredits.SetActive(true);
                canvasIntro.SetActive(false);
                canvasJeu.SetActive(false);
                canvasFin.SetActive(false);


                break;

            case ("Reglages"):
                // A partir du menu principal: 3 animations
                canvasReglages.SetActive(true);
                canvasIntro.SetActive(false);
                canvasJeu.SetActive(false);
                canvasFin.SetActive(false);

                break;

                break;
            case ("MenuPrincipal"):
                canvasIntro.SetActive(true);
                canvasJeu.SetActive(false);
                canvasFin.SetActive(false);
                canvasCredits.SetActive(false);
                canvasReglages.SetActive(false);

                break;
            case ("Fin"):
                canvasIntro.SetActive(false);
                canvasJeu.SetActive(false);
                canvasFin.SetActive(true);

                //GetComponent<AudioSource>().PlayOneShot(fin);

                break;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nomDeScene);

        fin = false;

        yield return new WaitForSeconds(1f);
        StartCoroutine(fonduAuVisible());
    }

    private IEnumerator fonduAuNoir()
    {
        ecranNoir.gameObject.SetActive(true);
        float tempsPasser = 0;
        Color couleur = imageNoire.color;

        // On met l'image de plus en plus noir (Moins en moins invisible) 
        while (tempsPasser < durationFondu)
        {
            couleur.a = Mathf.Clamp01(tempsPasser / durationFondu);
            imageNoire.color = couleur;
            tempsPasser += Time.deltaTime;
            yield return null; // Aka attend la prochaine frame et non un temps X 
        }

        // Invisibiliter a 100% a la fin peut importe quoi
        couleur.a = 1;
        imageNoire.color = couleur;
    }

    private IEnumerator fonduAuVisible()
    {
        ecranNoir.gameObject.SetActive(true);
        float tempsPasser = 0;
        Color couleur = imageNoire.color;
        couleur.a = 1;
        imageNoire.color = couleur;

        // On met l'image de moins en moins (Plus en plus invisible) 
        while (tempsPasser < durationFondu)
        {
            couleur.a = Mathf.Clamp01(1 - (tempsPasser / durationFondu));
            imageNoire.color = couleur;
            tempsPasser += Time.deltaTime;
            yield return null; // Aka attend la prochaine frame et non un temps X 
        }

        // Invisibiliter a 0% a la fin peut importe quoi
        couleur.a = 0;
        imageNoire.color = couleur;
        ecranNoir.gameObject.SetActive(false);
    }
}
