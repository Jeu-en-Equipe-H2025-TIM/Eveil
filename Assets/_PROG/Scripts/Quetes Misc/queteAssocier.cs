using UnityEngine;

public class queteAssocier : MonoBehaviour
{
    public GameObject questsManager;
    public int queteAssocierNumero;
    [SerializeField] public string queteAssocieeNom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // En bref: On donne le num de la quete de la liste à queteAssocierNumero dans l'inspecteur et ca nous sort le nom de la quete pour rendre la job d'integration 
        // et de manipulation des zones/objets qui trigger les quetes plus facile
        questsManager = GameObject.Find("questsManager");
        queteAssocieeNom = questsManager.GetComponent<questsManager>().listeQuetes[queteAssocierNumero];
    }
}
