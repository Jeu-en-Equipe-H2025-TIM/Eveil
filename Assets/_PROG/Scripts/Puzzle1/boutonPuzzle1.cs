using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class boutonPuzzle1 : MonoBehaviour
{
    // Variables boutons generales
    private bool proximiteJoueur;
    private bool doSomething;
    private bool canGoOffline = false;
    private bool outlineIsOn = false;


    // Variables puzzle
    public int valeur;
    private int stackWindow;
    public bool peutJouer;

    public GameObject affichageLier;
    public GameObject puzzleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        stackWindow = puzzleManager.GetComponent<puzzle1Manager>().stack;
        peutJouer = puzzleManager.GetComponent<puzzle1Manager>().statusPuzzle;
        affichageLier.GetComponent<TextMeshPro>().text = valeur.ToString();



        if (gameObject.GetComponent<objetProximity>().actif)
            {

                if (peutJouer)
                {
                    if (!outlineIsOn)
                    {
                        this.GetComponent<Outline>().enabled = true;
                        outlineIsOn = true;
                    }

                    if (gameObject.GetComponent<objetProximity>().doSomething)
                    {
                        if (valeur == (stackWindow + 1))
                        {
                            // L'OBJET FAIT QUELQUE CHOSE

                            Invoke("interactionBouton", 0f);

                        }
                        else
                        {
                            // Pas son tour (ordre d'appui)
                            Debug.Log("Je " + (this.gameObject.name) + " ne suis pas le bon bouton" + " | Valeur = " + valeur + " Stack = " + stackWindow);

                            puzzleManager.GetComponent<puzzle1Manager>().stack = 0;
                            this.GetComponent<MeshRenderer>().material.color = Color.black;

                            Invoke("fermerBouton", 1f);
                        }

                        gameObject.GetComponent<objetProximity>().doSomething = false;
                    }
                }




                canGoOffline = true;
            }
            else if (canGoOffline)
            {
                Debug.Log("Je " + (this.gameObject.name) + " suis offline");
                this.GetComponent<MeshRenderer>().material.color = Color.red;

                if (outlineIsOn)
                {
                    this.GetComponent<Outline>().enabled = false;
                    outlineIsOn = false;
                }


                canGoOffline = false;
            }
        
    
    }

    void interactionBouton()
    {
        Debug.Log("Je " + (this.gameObject.name) + " suis appuyer");


        this.GetComponent<MeshRenderer>().material.color = Color.yellow;

        puzzleManager.GetComponent<puzzle1Manager>().stack += 1;



        Invoke("fermerBouton", 1f);
    }

    void fermerBouton()
    {
        Debug.Log("Je " + (this.gameObject.name) + " ferme apres appuyage");
        gameObject.GetComponent<objetProximity>().doSomething = false;


        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
