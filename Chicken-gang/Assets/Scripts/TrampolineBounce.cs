using UnityEngine;

public class Trampo : MonoBehaviour
{
    public float ForceRebond = 15f; // Force du rebond

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Vérifie si c'est le joueur qui entre en contact
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Applique une vélocité verticale pour simuler le rebond
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, ForceRebond);
            }
        }
    }
}
