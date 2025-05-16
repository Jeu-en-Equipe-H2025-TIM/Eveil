using UnityEngine;

public class playerAttaque : MonoBehaviour
{
    // attaqueHitbox contient 3 objets avec le tag hitbox + des trigger colliders. Les ennemis/autres vont se faire prendre du dommage quand ils entrent en onTriggerEnter avec des choses de Tag "hitbox"
    public GameObject attaqueHitbox;
    public float attaqueCooldown;
    public float tempsAttaque;
    private bool pretAAttaquer = true;

    // Animations
    public GameObject modele;
    private Animator animator;

    public AudioClip sonAttaque; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = modele.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attaque()
    {
        if (pretAAttaquer)
        {
            // On gere le cooldown
            pretAAttaquer = false;
            Invoke("peutAttaquerEncore", attaqueCooldown);

            // Son d'attaque: 
            this.GetComponent<AudioSource>().PlayOneShot(sonAttaque);

            // Animation d'attaque: 
            animator.SetTrigger("attaque");

            // On active la hitbox
            attaqueHitbox.SetActive(true);
            // On désactive la hitbox après 0.5 secondes
            Invoke("desactiverHitbox", tempsAttaque);

        }
    }

    void desactiverHitbox()
    {
        // On désactive la hitbox
        attaqueHitbox.SetActive(false);
    }

    void peutAttaquerEncore()
    {
        pretAAttaquer = true;
    }


}