using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement
    public float jumpForce = 10f; // Force de saut

    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator
    private bool isGrounded; // Vérifie si le joueur est au sol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D attaché au player
        animator = GetComponent<Animator>(); // Récupère l'Animator attaché au player
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

        // Détecte si le joueur se déplace ou est à l'arrêt
        bool isCurrentlyMoving = Mathf.Abs(move) > 0;

        // Mise à jour du paramètre "isMoving" dans l'Animator
        animator.SetBool("isMoving", isCurrentlyMoving);

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
        animator.SetTrigger("Jump"); // Déclenche l'animation de saut
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Si le joueur touche le sol
        {
            isGrounded = true; // Le joueur est de nouveau au sol
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Quand le joueur quitte le sol
        {
            isGrounded = false; // Le joueur n'est plus au sol
        }
    }
}
