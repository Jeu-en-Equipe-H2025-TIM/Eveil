using UnityEngine;

public class ApparaitPlayerCine : MonoBehaviour
{
    public GameObject joueur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("ApparaitJoueur", 19f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApparaitJoueur()
    {
        joueur.SetActive(true);
    }
}
