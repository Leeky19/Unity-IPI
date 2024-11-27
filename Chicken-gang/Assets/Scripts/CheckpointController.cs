using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 15f; // Force du rebond

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // V�rifie si c'est le joueur qui entre en contact
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Applique une v�locit� verticale pour simuler le rebond
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);

                // Ajoute des effets visuels ou sonores ici si n�cessaire
                Debug.Log("Le joueur a utilis� le trampoline !");
            }
        }
    }
}
