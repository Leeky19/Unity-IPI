using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement
    public float jumpForce = 10f; // Force de saut

    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator
    private bool isGrounded; // Vérifie si le joueur est au sol
    private Vector3 startPosition; // Position initiale du joueur (GameObject Start)
    private Vector3 checkpointPosition; // Position du checkpoint
    public int fruitCount = 0; // Compteur de fruits collectés

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D attaché au player
        animator = GetComponent<Animator>(); // Récupère l'Animator attaché au player

        // Recherche le GameObject avec le tag "Start" et prend sa position comme position initiale
        GameObject startObject = GameObject.FindWithTag("Start");
        if (startObject != null)
        {
            startPosition = startObject.transform.position; // Enregistre la position du Start
        }
        else
        {
            Debug.LogError("Aucun GameObject avec le tag 'Start' trouvé !");
            startPosition = transform.position; // En cas d'absence de Start, utilise la position actuelle comme fallback
        }

        checkpointPosition = startPosition; // Au départ, le checkpoint est la position de départ
        transform.position = startPosition; // Force le joueur à se positionner au GameObject Start dès le début
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
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

        if (collision.CompareTag("Checkpoint")) // Si le joueur touche un checkpoint
        {
            checkpointPosition = collision.transform.position; // Met à jour la position du checkpoint
            Debug.Log("Checkpoint atteint !");
        }
    }

    private void RespawnPlayer()
    {
        // Utilise la position du checkpoint pour le respawn
        transform.position = checkpointPosition; //Si plusieurs checkpoint, dernier à être sauvegarder
        rb.linearVelocity = Vector2.zero;
    }

    public void CollectFruit()
    {
        fruitCount++; // Incrémente le compteur
        Debug.Log("Fruits collectés : " + fruitCount);
    }
}
