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

    [SerializeField] private GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("gameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hitbox")
        {

            if (qteVie > 0)
            {
                // Ennemi touché
                StartCoroutine("AttaquerParJoueur");
            }
  
        }
    }

    IEnumerator AttaquerParJoueur()
    {
        // Perte de la vie
        qteVie -= 1;
        // Changer la couleur en rouge
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        // Revenir à la couleur normale
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
                    objet.GetComponent<Animator>().SetBool("finBonne", true);

                }

                Debug.Log("Fin bonne! On active la fin dans X secondes!");


                Invoke("finDuJeu", delaisAvantFinDuJeu);
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
}
