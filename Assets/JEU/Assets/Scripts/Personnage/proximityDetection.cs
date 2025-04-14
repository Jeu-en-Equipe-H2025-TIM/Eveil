using UnityEngine;

public class proximityDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Variable pour montrer au dev que le joueur est proche d'un objet et peut intéragir
    [SerializeField] private bool canInteract = false;


    private void OnTriggerEnter(Collider other)
    {
        // Si il entre dans le cercle d'interaction (sphere collider Trigger ON) d'un objet
        if (other.gameObject.tag == "interactable")
        {
            other.GetComponent<objetProximity>().actif = true;


            canInteract = true;
        }

        // Si il entre dans le cercle d'interaction (sphere collider Trigger ON) d'un ennemi
        if (other.gameObject.tag == "ennemi")
        {
            // Joueur se fait pousser vers l'arriere (on essai d'attaquer le joueur)
            other.GetComponent<ennemiComportement>().atteintJoueur();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "interactable")
        {
            other.GetComponent<objetProximity>().actif = false;

            canInteract = false;
        }


    }
}
