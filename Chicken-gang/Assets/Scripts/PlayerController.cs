using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement
    public float jumpForce = 10f; // Force de saut

    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator
    private bool isGrounded; // Vérifie si le joueur est au sol
    private Vector3 startPosition; // Position initiale du joueur

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D attaché au player
        animator = GetComponent<Animator>(); // Récupère l'Animator attaché au player
        startPosition = transform.position; // Enregistre la position de départ
    }

    void Update()
    {
        MovePlayer(); // Appelle la fonction de déplacement
        if (isGrounded && Input.GetButtonDown("Jump")) // Si au sol et appuie sur "Espace"
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float move = Input.GetAxis("Horizontal"); // Déplacement horizontal
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y); // Applique la vélocité horizontale

        // Empêche l'animation de se jouer si le joueur saute
        if (isGrounded)
        {
            animator.SetBool("isMoving", Mathf.Abs(move) > 0); // Mise à jour de l'animation de mouvement
        }

        // Inverser le sprite selon la direction
        if (move > 0)
        {
            transform.localScale = new Vector3(-3, 3, 3); // Orientation vers la droite
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(3, 3, 3); // Orientation vers la gauche
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Applique la force de saut

        // Désactiver les animations pendant le saut
        animator.SetBool("isMoving", false); // Arrête toute animation de mouvement
        animator.SetTrigger("Jump"); // Si tu as une animation de saut, déclenche-la
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Permet au joueur de détecter le sol ou la boxe pour être considéré au sol
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true; // Le joueur est au sol
            animator.ResetTrigger("Jump"); // Réinitialise l'animation de saut
            animator.SetBool("isMoving", Mathf.Abs(rb.linearVelocity.x) > 0); // Réactive l'animation de mouvement
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = false; // Le joueur quitte le sol
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap")) // Vérifie si le joueur touche un piège
        {
            RespawnPlayer(); // Ramène le joueur à la position de départ
        }
    }

    private void RespawnPlayer()
    {
        transform.position = startPosition; // Replace le joueur à la position initiale
        rb.linearVelocity = Vector2.zero; // Réinitialise la vitesse du joueur
    }
}
