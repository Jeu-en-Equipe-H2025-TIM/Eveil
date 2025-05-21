using UnityEngine;

public class Anim1Cam : MonoBehaviour
{
    public GameObject joueur;
    //public GameObject uiJeu;
    public GameObject camAnim1;
    public GameObject CamPersoAnim1;

    public GameObject gamemanager;

    public GameObject animeBras;
    public GameObject parentAnimationBrasDebut;
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamemanager = GameObject.Find("gameManager");

        
        if (gamemanager.GetComponent<gameManager>().premiereFois == false)
        {
            CamPersoAnim1.SetActive(true);
            animeBras.SetActive(true);
            parentAnimationBrasDebut.SetActive(true);


            Invoke("OnCam1", 11f);
            Invoke("ApparaitJoueur", 19f);
            Debug.Log("premiereFois Anim1CAm");

            gamemanager.GetComponent<gameManager>().premiereFois = true;
        }
        else
        {
            parentAnimationBrasDebut.SetActive(false);
            animeBras.SetActive(false);
            Debug.Log("Apparait Joueur Retour au Jeu");

            camAnim1.SetActive(false);
            Invoke("ApparaitJoueur", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCam1()
    {
        CamPersoAnim1.SetActive(false);
        camAnim1.SetActive(true);
        
    }
    public void ApparaitJoueur()
    {
        Debug.Log("Apparait Joueur");
        joueur.SetActive(true);
        //uiJeu.SetActive(true);
        camAnim1.SetActive(false);

    }
}
