using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class finBonne : MonoBehaviour
{
    public List<GameObject> listeObjetsAnimations = new List<GameObject>();
    public int delaisAvantFinDuJeu;

    public int qteVie = 3;
    private bool lancementFinDuJeu = false;


    // Dialogues
    public GameObject dialogueManager;
    public int positionDuTexteDansLaListeLineaire1;
    public int dureeSurEcran1;
    public int positionDuTexteDansLaListeLineaire2;
    public int dureeSurEcran2;
    public int positionDuTexteDansLaListeLineaire3;
    public int dureeSurEcran3;

     public GameObject audioManager;

    public AudioClip dialogueAudio1;
    public AudioClip dialogueAudio2;
    public AudioClip dialogueAudio3;

    public AudioClip vitreBrise;
    public AudioClip audioFin;

    private GameObject gameManager;

    // Animation
    public GameObject animationCamera;
    public GameObject joueur;
    public GameObject modele;
    private Animator animatorModele;

    void Start()
    {
        gameManager = GameObject.Find("gameManager");
        audioManager = GameObject.Find("audiosManager");
        dialogueManager = GameObject.Find("dialoguesManager");
        animatorModele = modele.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hitbox")
        {

            if (qteVie > 0)
            {
                // Ennemi touch�
                StartCoroutine("AttaquerParJoueur");
            }

        }
    }

    IEnumerator AttaquerParJoueur()
    {
        // Perte de la vie
        qteVie -= 1;

        // Dialogues

        if (qteVie == 2)
        {
            dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger("lineaire", positionDuTexteDansLaListeLineaire1, dureeSurEcran1, dialogueAudio1);
            animatorModele.SetTrigger("no");
        }

        if (qteVie == 1)
        {
            dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger("lineaire", positionDuTexteDansLaListeLineaire2, dureeSurEcran2, dialogueAudio2);
            animatorModele.SetTrigger("no");
        }

        if (qteVie == 0)
        {
            dialogueManager.GetComponent<dialoguesManager>().dialogueTrigger("lineaire", positionDuTexteDansLaListeLineaire3, dureeSurEcran3, dialogueAudio3);
            animatorModele.SetTrigger("no");
            this.GetComponent<AudioSource>().PlayOneShot(vitreBrise);
        }




        // Changer la couleur en rouge
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        // Revenir � la couleur normale
        this.GetComponent<MeshRenderer>().material.color = Color.white;



        yield return default;
    }


    void Update()
    {
        if (qteVie <= 0)
        {


            if (!lancementFinDuJeu)
            {
                lancementFinDuJeu = true;
                Debug.Log("Fin bonne! On met les bools des animations en true");
                foreach (GameObject objet in listeObjetsAnimations)
                {
                    objet.GetComponent<Animator>().SetBool("GoodEnd", true);

                }

                Debug.Log("Fin bonne! On active la fin dans X secondes!");

                // Trucs d'animations
                gameManager.GetComponent<menus>().fondus(26f);
                Invoke("animation", 1f);
                Invoke("finDuJeu", delaisAvantFinDuJeu);
                audioManager.GetComponent<AudioSource>().clip = audioFin;
                audioManager.GetComponent<AudioSource>().Play();
            }
        }
    }



    private void finDuJeu()
    {

        Debug.Log("Fin bonne! Fin du jeu!");
        lancementFinDuJeu = false; // Reset le status
        qteVie = 3; // Reset la vie


        gameManager.GetComponent<gameManager>().procedureReset = true; // Lance la procedure de reset (qui va delete tous les anciens game managers) au changement de scene
        gameManager.GetComponent<menus>().ouverture(); // Change la scene
    }



    private void animation()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        animationCamera.SetActive(true);
        joueur.SetActive(false);
    }
}