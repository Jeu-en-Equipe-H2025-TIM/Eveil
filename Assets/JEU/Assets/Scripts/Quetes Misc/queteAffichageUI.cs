using TMPro;
using UnityEngine;

public class queteAffichageUI : MonoBehaviour
{

    public GameObject questManager;

    private Color couleurDuTexte = Color.white;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questManager = GameObject.Find("questsManager");
        this.GetComponent<TextMeshProUGUI>().text = questManager.GetComponent<questsManager>().listeQuetes[0];
    }

    // Update is called once per frame
    void Update()
    {
        couleurDuTexte = questManager.GetComponent<questsManager>().couleurDuTexte;
        this.GetComponent<TextMeshProUGUI>().color = couleurDuTexte;
        this.GetComponent<TextMeshProUGUI>().text = questManager.GetComponent<questsManager>().listeQuetes[0];
    }
}
