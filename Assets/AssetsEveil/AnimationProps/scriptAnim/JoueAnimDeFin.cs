using UnityEngine;

public class JoueAnimDeFin : MonoBehaviour
{
    public Animator AnimJouerAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void JouerAnimMauvaiseFin()
    {
        AnimJouerAI.SetBool("BadEnd", true);
    }

    void JouerAnimBonneFin()
    {
        AnimJouerAI.SetBool("GoodEnd", true);
    }
}
