using UnityEngine;

public class porteOuverture : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool canGoOffline = false;

    private bool porteDisponible = true;
    private bool porteStatus = false;

    public AudioClip audioPorteOuvre;





    // Update is called once per frame
    void Update()
    {
        proximiteJoueur = gameObject.GetComponent<objetProximity>().actif;





        if (proximiteJoueur)
        {
            // Porte ferme
            if (porteDisponible && !porteStatus)
            {
                Debug.Log("Porte ouvre");
                Invoke("ouvrirPorte", 0f);
            }

        } else if (canGoOffline)
        {
            // Porte ferme
            if (porteDisponible && porteStatus)
            {
                Debug.Log("Porte ouvre");
                Invoke("fermerPorte", 0f);
            }
        }
    }

    public void ouvrirPorte()
    {

        Debug.Log("Invoke Porte ouvre");

        porteDisponible = false;
        Invoke("rendrePorteDisponible", 1f);
        this.GetComponent<AudioSource>().PlayOneShot(audioPorteOuvre);


        // mettre animation ouverture
        this.GetComponent<Animator>().SetTrigger("OuvrirDoor");

        canGoOffline = true;
    }

    public void fermerPorte()
    {
        Debug.Log("Invoke Porte ouvre");

        porteDisponible = false;
        Invoke("rendrePorteDisponible", 1f);
        this.GetComponent<AudioSource>().PlayOneShot(audioPorteOuvre);


        // mettre animation fermeture
        this.GetComponent<Animator>().SetTrigger("FermerDoor");

        canGoOffline = false;
    }

    public void rendrePorteDisponible()
    {

        Debug.Log("Rend Porte Disponible");

        porteDisponible = true;
        porteStatus = !porteStatus;
    }
}
