using UnityEngine;

public class Trampo : MonoBehaviour
{
    public float ForceRebond = 15f; // Force du rebond
    private Animator animator; // Référence à l'Animator

    void Start()
    {
        // Récupère l'Animator attaché à l'objet
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Aucun Animator trouvé sur l'objet Trampoline !");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Applique une vélocité verticale pour simuler le rebond
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, ForceRebond);

                // Déclenche l'animation de saut
                if (animator != null)
                {
                    animator.SetTrigger("Jump");
                }

                // Optionnel : Debug pour confirmer l'exécution
                Debug.Log("Le joueur a rebondi sur le trampoline !");
            }
        }
    }
}
