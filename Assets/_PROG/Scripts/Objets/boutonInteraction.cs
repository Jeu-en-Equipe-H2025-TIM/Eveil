using UnityEngine;

public class boutonInteraction : MonoBehaviour
{
    private bool proximiteJoueur;
    private bool doSomething;


    private bool canGoOffline = false;
    private bool outlineIsOn = false;


    // Update is called once per frame
    void Update()
    {




        if (gameObject.GetComponent<objetProximity>().actif)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.green;

            if (!outlineIsOn)
            {
                this.GetComponent<Outline>().enabled = true;


                outlineIsOn = true;
            }

            if (gameObject.GetComponent<objetProximity>().doSomething)
            {
                Debug.Log("Je " + (this.gameObject.name) + " fait quelque chose");

                // L'OBJET FAIT QUELQUE CHOSE
                this.GetComponent<MeshRenderer>().material.color = Color.yellow;




                gameObject.GetComponent<objetProximity>().doSomething = false;
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
}
