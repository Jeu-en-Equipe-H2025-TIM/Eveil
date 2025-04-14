using TMPro;
using UnityEngine;

public class queteAffichageUI : MonoBehaviour
{

    public GameObject questManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questManager = GameObject.Find("questsManager");
        this.GetComponent<TextMeshProUGUI>().text = questManager.GetComponent<questsManager>().listeQuetes[0];
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = questManager.GetComponent<questsManager>().listeQuetes[0];
    }
}
