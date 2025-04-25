using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.CompilerServices;

public class ennemiComportement : MonoBehaviour
{

    // Joueur
    public GameObject cible; // joueur
    private Transform cibleTransform;

    // Deplacement
    public Transform destination;
    public float chaseDistance = 0;
    private Boolean rechercheNouvelleDestination = true;
    private float randomWaitTime;
    private Vector3 directionEntreMoiEtCible;

    // Zone de mouvement
    NavMeshAgent navMeshAgent;
    public Transform zoneRepos;
    public Transform zoneCoinTopLeft;
    public float zoneTaille;

    // Pushback
    public float forcePushback;
    public float durationPushback;


    // Vie
    public float qteVie;

    // Si l'on veut que l'ennemi navigue à travers une liste de points déterminés à l'avance, de-comment ces deux variables
    //public List<Transform> listesDePointsDeRepos;
    //int pointer = 0;

    public bool peutAttaquer = true;

    // Start is called before the first frame update
    void Start()
    {
        // Recuperation de variables (+ Setup ) 
        navMeshAgent = GetComponent<NavMeshAgent>();
        //zoneRepos = listesDePointsDeRepos[pointer]; // Si l'on veut que l'ennemi navigue à travers une liste de points déterminés à l'avance, de-comment ceci

        cibleTransform = cible.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // On veut savoir la distance restante + vitesse actuelle + direction de mouvement en permanence
        directionEntreMoiEtCible = cibleTransform.position - transform.position;
        float distance = directionEntreMoiEtCible.magnitude;
        float vitesse = navMeshAgent.velocity.y;

        // Chase, si non navigue aléatoirement dans une zone
        if (distance < chaseDistance)
        {
            navMeshAgent.SetDestination(cibleTransform.position);
        }
        else
        {
            if (navMeshAgent.remainingDistance == 0 && rechercheNouvelleDestination)
            {
                rechercheNouvelleDestination = false;
                randomWaitTime = UnityEngine.Random.Range(0f, 2f);
                Invoke("nouvelleDestination", randomWaitTime);
            }
        }


        // Chase, si non navigue les points de repos aléatoirement (points de repos set-up en avance, décidé par le dev)
        //if (distance < chaseDistance )
        //{
        //    navMeshAgent.SetDestination(cible.position);
        //} else
        //{
        //    navMeshAgent.SetDestination(zoneRepos.position);

        //    if (navMeshAgent.remainingDistance == 0 )
        //    {
        //        pointer = Random.Range(0, 3);
        //        print(pointer);
        //        zoneRepos = listesDePointsDeRepos[pointer];
        //    }
        //}



    }

    void nouvelleDestination()
    {
        // On donne la nouvelle destination et permet la recherche de la prochaine destination
        destination.position = new Vector3(UnityEngine.Random.Range(zoneCoinTopLeft.position.x, zoneCoinTopLeft.position.x + zoneTaille), zoneCoinTopLeft.position.y, UnityEngine.Random.Range(zoneCoinTopLeft.position.z, zoneCoinTopLeft.position.z + zoneTaille));
        navMeshAgent.SetDestination(destination.position); // Pourrait être en 1 ligne (celle-ci + celle du dessus)
        rechercheNouvelleDestination = true;
    }

    public void atteintJoueur()
    {
        if (peutAttaquer)
        {
            peutAttaquer = false; // Ne peut pas re-attaquer
            StartCoroutine("PousseJoueur", directionEntreMoiEtCible);
        }
    }


    IEnumerator PousseJoueur(Vector3 direction)
    {

        // Logique de push-back
        // Ne peut pas re-utiliser le rigidbody can on controle les mouvements par characterController + les deux ne jouent pas bien ensemble
        // CharacterController.Move() donne le sentiment de teleportation quand fait en 1 fois, donc co-routine pour le faire progressivement (effet de push-back illusoire)

        StartCoroutine(IncrementationDeLaPoussee(direction, forcePushback, durationPushback));
        

        yield return new WaitForSeconds(0.2f); // Pause pour laisser le joueur se faire push-back
        peutAttaquer = true; // Peut re-attaquer
        yield return default;
    }

    IEnumerator IncrementationDeLaPoussee(Vector3 direction, float force, float duration)
    {
        float tempsPasser = 0;
        float vitesse = force / duration; // On veut une vitesse constante

        while (tempsPasser < duration)
        {
            cible.GetComponent<CharacterController>().Move(direction * vitesse * Time.deltaTime); // on pushback
            tempsPasser += Time.deltaTime; // on incremente le temps passé
            yield return null; // return null attend 1 frame
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hitbox")
        {

            if (qteVie > 0)
            {
                // Ennemi touché
                StartCoroutine("AttaquerParJoueur");

            } else
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator AttaquerParJoueur()
    {
        // Perte de la vie
        qteVie -= 1;
        // Freeze temporaire (0.3s)
        this.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<NavMeshAgent>().enabled = true;

        yield return default;
    }
}