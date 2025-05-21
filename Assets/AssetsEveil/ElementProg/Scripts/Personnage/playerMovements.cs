
// Your editor-specific code here

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
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


    private float vitesseRotation;

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

    // Animations
    private Animator animator;
    private bool seDeplace = false;
    public GameObject modele;

    // Son 
    private bool sonActif = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        gameManager = GameObject.Find("gameManager");
        characterController.enabled = false;
        Invoke("placeJoueur", 0.5f);

        animator = modele.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        vitesseRotation = PlayerPrefs.GetFloat("sensibiliteSouris", 110);
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


        // Si on se d�place
        if (deplacement.magnitude > 0.1f)
        {
            if (!seDeplace)
            {
                seDeplace = true;
                Debug.Log("Deplacement animation");
                this.GetComponent<AudioSource>().Play();
                animator.SetBool("marche", true);
            }
        }
        else // On ne se d�place pas
        {
             if (seDeplace)
            {
                seDeplace = false;
                Debug.Log("Deplacement animation ARRET");
                this.GetComponent<AudioSource>().Pause();
                animator.SetBool("marche", false);
            }
        }

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


    public void placeJoueur()
    {
        Debug.Log("Set ma position: " + gameManager.GetComponent<gameManager>().positionJoueurPublique);
        this.transform.position = gameManager.GetComponent<gameManager>().positionJoueurPublique;
        characterController.enabled = true;
    }
}




