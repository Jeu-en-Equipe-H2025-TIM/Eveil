using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finAI : MonoBehaviour
{
    public List<GameObject> listeObjetsAnimations = new List<GameObject>();
    public float delaisAvantFinDuJeu;

    [SerializeField] private GameObject gameManager;


    //// Tous les managers 
    //private GameObject Canvas;
    //private GameObject audiosManager;
    //private GameObject questsManager;
    //private GameObject dialoguesManager;


    // Dialogues
    private GameObject dialoguesManager;
    public int positionDuTexteDansLaListeLineaire1;
    public int dureeSurEcran1;
    public AudioClip dialogueAudio;


    // Animation
    public GameObject animationModel;
    public GameObject animationCamera;
    public GameObject joueur;

    void Start()
    {
        gameManager = GameObject.Find("gameManager");
        dialoguesManager = GameObject.Find("dialoguesManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
            foreach (GameObject objet in listeObjetsAnimations)
            {
                objet.GetComponent<Animator>().SetBool("finAI", true);
            }

            // Dialogue de fin 
            dialoguesManager.GetComponent<dialoguesManager>().dialogueTrigger("lineaire", positionDuTexteDansLaListeLineaire1, dureeSurEcran1, dialogueAudio);

            // Trucs d'animations
            gameManager.GetComponent<menus>().fondus(26f);
            Invoke("animation", 1f);
            Invoke("finDuJeu", delaisAvantFinDuJeu);
        }
    }


    private void finDuJeu()
    {

        gameManager.GetComponent<gameManager>().procedureReset = true; // Lance la procedure de reset (qui va delete tous les anciens game managers) au changement de scene
        gameManager.GetComponent<menus>().ouverture(); // Change la scene
    }

    private void animation()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        animationCamera.SetActive(true);  
        animationModel.SetActive(true);
        joueur.SetActive(false);
    }
}