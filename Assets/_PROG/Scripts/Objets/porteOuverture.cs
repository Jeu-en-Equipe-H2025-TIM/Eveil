using UnityEngine;

public class porteOuverture : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool canGoOffline = false;





    // Update is called once per frame
    void Update()
    {
        proximiteJoueur = gameObject.GetComponent<objetProximity>().actif;




        if (proximiteJoueur)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;

            canGoOffline = true;

        } else if (canGoOffline)
        {
            Debug.Log("Je " + (this.gameObject.name) + " suis offline");
            this.GetComponent<MeshRenderer>().material.color = Color.red;

            canGoOffline = false;
        }
    }
}
