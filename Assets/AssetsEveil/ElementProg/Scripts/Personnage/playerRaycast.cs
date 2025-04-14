using UnityEngine;

public class playerRaycast : MonoBehaviour
{

    public float distanceActivationMinimal; // Distance minimal pour interagir avec les objets
    bool estProche = false; // Si on est proche: true. Sinon: false

    public Camera camera; // Camera du joueur



    public GameObject lastObjetVu = null;

    public GameObject objetVu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = this.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make a raycast towards the front of the head
        RaycastHit hit;

        estProche = Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, distanceActivationMinimal, ~LayerMask.GetMask("joueur"));
        Debug.DrawLine(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * distanceActivationMinimal, Color.red);

        if (estProche)
        {
            objetVu = hit.collider.gameObject;

            if (objetVu.GetComponent<objetProximity>() != null ) {
                if (objetVu.GetComponent<objetProximity>().actif && (Input.GetKeyDown(KeyCode.E)))
                {
                    Debug.Log("Objet vu interagit avec: " + objetVu.name);
                    objetVu.GetComponent<objetProximity>().doSomething = true;
                }
                else
                {
                    objetVu = null;
                }
            }
        }


    }
}
