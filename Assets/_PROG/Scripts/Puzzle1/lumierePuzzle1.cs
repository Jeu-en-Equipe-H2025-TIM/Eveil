using UnityEngine;

public class lumierePuzzle1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int value;
    public int stackWindow;
    public GameObject puzzleManager;


    // Update is called once per frame
    void Update()
    {
        stackWindow = puzzleManager.GetComponent<puzzle1Manager>().stack;

        if (value <= stackWindow)
        {
            /* Lumiere allumer! */
            this.GetComponent<MeshRenderer>().material.color = Color.green;
        } else
        {
            /* Lumiere eteinte! */
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
