using UnityEngine;

public class animDebutJouer : MonoBehaviour
{
    public Animator AnimJouerPerso;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void JouerAnim()
    {
        AnimJouerPerso.SetBool("JoueAnimDebut", true);
    }
}
