using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class playerMovements : MonoBehaviour
{
    CharacterController characterController;
    public GameObject gameManager;

    // Variables Deplacement
    Vector3 deplacement;
    float vitesseDeplacement;
        private bool courseActive = false;
        public float vitesseDeplacementBase;
        public float multiplicateurMarche;
        public float multiplicateurCourse;


    public float vitesseRotation;

    float deplacementVertical;
        public float gravity = 9.81f;
        Boolean estAuSol;
        public float forceSaut;
    //

    // Variables Autre
    public Camera cam;
    public GameObject tete;
    float rotationVerticaleCam;
    public float rotationVerticaleCameraAngleMin;
    public float rotationVerticaleCameraAngleMax;
    float rotationHorizontale;

    //


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        gameManager = GameObject.Find("gameManager");
        characterController.enabled = false;
        Invoke("placeJoueur", 0.5f);


    }

    // Update is called once per frame
    void Update()
    {
        // INPUTS MOUVEMENTS HORIZONTAUX 
            if (Input.GetKey(KeyCode.LeftShift))
            {
            courseActive = true;
            }
            else
            {
            courseActive = false;
            }
        //




    }

    // Utilisation du FixedUpdate car nous avons un character controller. 
    void FixedUpdate()
    {

        // MOUVEMENTS HORIZONTAUX
            deplacement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 directionLocale = transform.TransformDirection(deplacement);

            if (courseActive)
            {
                vitesseDeplacement = vitesseDeplacementBase * multiplicateurCourse;
            }
            else
            {
                vitesseDeplacement = vitesseDeplacementBase * multiplicateurMarche;
            }
        //


        // MOUVEMENTS VERTICAUX
            estAuSol = characterController.isGrounded;
            deplacementVertical -= gravity * Time.deltaTime;

            // If grounded -> Set vertical speed to negative to prevent any issues
            if (estAuSol && deplacementVertical < 0f)
            {
                deplacementVertical = -1f;
            }

            directionLocale.y = deplacementVertical;
            //

            // MOUVEMENTS DE LA TETE (DONC CAMERA AKA VISION)
            rotationHorizontale = Input.GetAxis("Mouse X") * vitesseRotation * Time.deltaTime;
            rotationVerticaleCam += Input.GetAxis("Mouse Y") * vitesseRotation * Time.deltaTime;

            if (rotationVerticaleCam > rotationVerticaleCameraAngleMax)
            {
                rotationVerticaleCam = rotationVerticaleCameraAngleMax;
            }

            if (rotationVerticaleCam < rotationVerticaleCameraAngleMin)
            {
                rotationVerticaleCam = rotationVerticaleCameraAngleMin;
            }
        //

        // APPLICATION DU MOUVEMENT SUR LE CC (HORIZONTAL, VERTICAL) + TETE (VISION)
            characterController.Move(directionLocale * vitesseDeplacement * Time.deltaTime); // Vertical
            transform.Rotate(Vector3.up * rotationHorizontale); // Horizontal
            tete.transform.localRotation = Quaternion.Euler(-Mathf.Clamp(rotationVerticaleCam, rotationVerticaleCameraAngleMin, rotationVerticaleCameraAngleMax), 0f, 0f); // Vision
        //
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Non-trigger
    }

    public void OnTriggerEnter(Collider other)
    {
        // Trigger
    }

    public void placeJoueur()
    {
        Debug.Log("Set ma position: " + gameManager.GetComponent<gameManager>().positionJoueurPublique);
        this.transform.position = gameManager.GetComponent<gameManager>().positionJoueurPublique;
        characterController.enabled = true;
    }
}




