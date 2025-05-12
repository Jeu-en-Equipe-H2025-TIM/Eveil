using UnityEngine;

public class Anim1Cam : MonoBehaviour
{
    public GameObject joueur;
    //public GameObject uiJeu;
    public GameObject camAnim1;
    public GameObject CamPersoAnim1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Invoke("OnCam1", 11f);
        Invoke("ApparaitJoueur", 19f);
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
        joueur.SetActive(true);
        //uiJeu.SetActive(true);
        camAnim1.SetActive(false);
        
    }
}
