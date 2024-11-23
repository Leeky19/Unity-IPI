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

        // Recherche du GameObject avec le tag "Start"
        GameObject startPoint = GameObject.FindGameObjectWithTag("Start");
        if (startPoint != null)
        {
            transform.position = startPoint.transform.position; // Définit la position initiale du joueur
        }
        else
        {
            Debug.LogWarning("Aucun GameObject avec le tag 'Start' trouvé. La position actuelle sera utilisée comme départ.");
        }

        startPosition = transform.position; // Enregistre la position initiale
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

        if (isGrounded)
        {
            animator.SetBool("isMoving", Mathf.Abs(move) > 0); // Mise à jour de l'animation de mouvement
        }

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

        animator.SetBool("isMoving", false); // Arrête toute animation de mouvement
        animator.SetTrigger("Jump"); // Si animation de saut, déclenche
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
            animator.ResetTrigger("Jump");
            animator.SetBool("isMoving", Mathf.Abs(rb.linearVelocity.x) > 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
