using UnityEngine;

public class queteTriggerZone : MonoBehaviour
{
    public GameObject questManager;
    public bool relierAUneHallucination;
    public int delaisTriggerQueteEnSecondes;

    [SerializeField] private string queteAssociee;

    private void Start()
    {
        questManager = GameObject.Find("questsManager");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        queteAssociee = this.GetComponent<queteAssocier>().queteAssocieeNom;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // On active la quete associée à la zone
            // On peut aussi ajouter un message de confirmation
            Debug.Log("ZONE TRIGGER QUETE Detecte Joueur");

            if (questManager.GetComponent<questsManager>().listeQuetes[0] == queteAssociee)
            {
                questManager.GetComponent<questsManager>().queteTrigger(delaisTriggerQueteEnSecondes, relierAUneHallucination);
            } else
            {
                Debug.Log("Le joueur a skip une quete..?");
                Debug.Log("ZONE: Quete associee = " + queteAssociee);
                Debug.Log("ZONE: Quete [0] (actuelle) du manager = " + questManager.GetComponent<questsManager>().listeQuetes[0]);
            }

        }
    }
}
