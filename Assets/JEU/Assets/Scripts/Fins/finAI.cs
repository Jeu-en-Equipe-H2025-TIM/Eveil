using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finAI : MonoBehaviour
{
    public List<GameObject> listeObjetsAnimations = new List<GameObject>();
    public int delaisAvantFinDuJeu;

    private GameObject gameManager;



    // Tous les managers 
    private GameObject Canvas;
    private GameObject audiosManager;
    private GameObject questsManager;
    private GameObject dialoguesManager;


    void Start()
    {
        gameManager = GameObject.Find("gameManager");
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


            Invoke("finDuJeu", delaisAvantFinDuJeu);
        }
    }


    private void finDuJeu()
    {
 
        gameManager.GetComponent<gameManager>().procedureReset = true; // Lance la procedure de reset (qui va delete tous les anciens game managers) au changement de scene
        gameManager.GetComponent<menus>().ouverture(); // Change la scene
    }
}
