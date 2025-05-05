using UnityEngine;
using UnityEngine.LightTransport;

public class camerasRotation : MonoBehaviour
{

    public GameObject joueur;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(new Vector3(joueur.transform.position.x, this.transform.position.y , joueur.transform.position.z));
    }
}
