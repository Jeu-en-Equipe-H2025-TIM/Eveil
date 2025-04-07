using UnityEngine;

public class proximityDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private bool canInteract = false;

    void Start()
    {
    }



    private void OnTriggerEnter(Collider other)
    {

       if (other.gameObject.tag == "interactable")
       {
            other.GetComponent<objetProximity>().actif = true;

            
            canInteract = true;
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
