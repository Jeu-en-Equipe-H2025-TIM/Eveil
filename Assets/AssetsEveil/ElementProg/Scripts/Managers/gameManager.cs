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
    public GameObject joueur;
    public GameObject dialoguesManager;


    public static int timer;
     public static Vector3 positionSauvegarderJoueur; 
    public Vector3 positionJoueurPublique;
    private static gameManager instance;

    private bool timerIsRunning = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // positionSauvegarderJoueur = new Vector3(45.2f, -1f, -1075.9f);
        positionSauvegarderJoueur = new Vector3(-15.16f, 22.51f, -46.6813f);
        joueur = GameObject.Find("Player");
        
        // Pour utiliser les variables statiques
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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
        } else
        {
  
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
        } else
        {
            timerIsRunning = false;
        }

    }

    public void sauvegardePositionJoueur()
    {
        joueur = GameObject.Find("Player");
        positionSauvegarderJoueur = joueur.transform.position;

        Debug.Log("Position joueur sauvegard� "+positionSauvegarderJoueur);
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
