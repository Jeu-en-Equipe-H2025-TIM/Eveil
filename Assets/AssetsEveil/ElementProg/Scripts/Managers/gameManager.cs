
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class gameManager : MonoBehaviour
{
    // Objets DontDestroyOnLoad (qu'on va garder � travers les scenes)
    public Canvas canvas;
    public GameObject audioManager;
    public GameObject questsManager;
    public GameObject dialoguesManager;
    public GameObject joueur;



    public static int timer;
    public static int timerOld; // Permet d'afficher le "meilleur score" | Pas encore mis en jeu ni créer un moyen de le mettre en jeu, si le temps, bonus!!
    public static Vector3 positionSauvegarderJoueur;
    public Vector3 positionJoueurPublique;

    private bool timerIsRunning = false;

    public bool procedureReset = false;

    public bool premiereFois = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // positionSauvegarderJoueur = new Vector3(45.2f, -1f, -1075.9f);
        positionSauvegarderJoueur = new Vector3(-15.16f, 22.51f, -46.6813f);
        joueur = GameObject.Find("Player");



        // On met le nouveau set de managers en DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(audioManager);
        DontDestroyOnLoad(questsManager);
        DontDestroyOnLoad(dialoguesManager);
        // Comme �a, on n'a le canvas et gamemanager QU'UNE SEULE FOIS (duplication � cause du DontDestroyOnLoad) car on ne revient jamais � la scene "Ouverture"
        SceneManager.LoadScene("MenuPrincipal");
    }



    void Update()
    {

        positionJoueurPublique = positionSauvegarderJoueur;

        if (!timerIsRunning && SceneManager.GetActiveScene().name == "Jeu")
        {
            timerIsRunning = true;
            StartCoroutine(timerUp());
        }

        // On vient de la fin du jeu, donc on enleve ce set de DontDestroyOnLoad pour laisser la place au suivant 
        if (SceneManager.GetActiveScene().name == "Ouverture" && procedureReset)
        {
            procedureReset = false;

            if (timerOld > timer || timerOld == 0)
            {
                timerOld = timer;
            }

            timer = 0;

            Destroy(canvas.gameObject);
            Destroy(audioManager);
            Destroy(questsManager);
            Destroy(dialoguesManager);
            Destroy(gameObject);


        }

    }


    private IEnumerator timerUp()
    {
        if (SceneManager.GetActiveScene().name == "Jeu")
        {
            timer++;
            //Debug.Log("Timer == " + timer);
            yield return new WaitForSeconds(1);
            StartCoroutine(timerUp());
        }
        else
        {
            timerIsRunning = false;
        }

    }

    public void sauvegardePositionJoueur()
    {
        joueur = GameObject.Find("Player");
        positionSauvegarderJoueur = joueur.transform.position;

        Debug.Log("Position joueur sauvegard� " + positionSauvegarderJoueur);
    }

    public void placerJoueur()
    {
        joueur = GameObject.Find("Player");

        joueur.GetComponent<CharacterController>().enabled = false;
        joueur.transform.position = positionSauvegarderJoueur;
        joueur.GetComponent<CharacterController>().enabled = true;

        Debug.Log("D�placer joueur");
    }
}
