using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiInGameRevient : MonoBehaviour
{
    private GameObject UiInGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UiInGame = GameObject.Find("UI_Ingame");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UiShow()
    {
        UiInGame.SetActive(true);
    }
}
