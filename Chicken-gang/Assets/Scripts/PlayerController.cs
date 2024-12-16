using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement
    public float jumpForce = 10f; // Force de saut

    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Animator animator; // Référence à l'Animator
    private bool isGrounded; // Vérifie si le joueur est au sol
    private Vector3 startPosition; // Position initiale
    private Vector3 checkpointPosition; // Position du checkpoint
    public int fruitCount = 0; // Compteur de fruits collectés

    private float moveInput = 0f; // Contrôle du mouvement horizontal

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D attaché
        animator = GetComponent<Animator>(); // Récupère l'Animator attaché

        // Recherche le GameObject avec le tag "Start"
        GameObject startObject = GameObject.FindWithTag("Start");
        if (startObject != null)
        {
            startPosition = startObject.transform.position;
        }
        else
        {
            Debug.LogError("Aucun GameObject avec le tag 'Start' trouvé !");
            startPosition = transform.position;
        }

        checkpointPosition = startPosition; // Au départ, le checkpoint est la position initiale
        transform.position = startPosition; // Positionne le joueur au début
    }

    void Update()
    {
        // Appelle la fonction de déplacement
        MovePlayer();

        // Saut via le bouton ou la touche espace
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // Applique la vélocité horizontale
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (isGrounded)
        {
            animator.SetBool("isMoving", Mathf.Abs(moveInput) > 0);
        }

        // Orientation du joueur
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-3, 3, 3); // Vers la droite
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(3, 3, 3); // Vers la gauche
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isMoving", false);
        }
    }

    // Appelé par les boutons pour déplacer vers la gauche
    public void MoveLeft()
    {
        moveInput = -1f;
    }

    // Appelé par les boutons pour déplacer vers la droite
    public void MoveRight()
    {
        moveInput = 1f;
    }

    // Appelé pour arrêter le mouvement lorsque le bouton est relâché
    public void StopMoving()
    {
        moveInput = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
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

        if (collision.CompareTag("Checkpoint"))
        {
            checkpointPosition = collision.transform.position;
            Debug.Log("Checkpoint atteint !");
        }
    }

    private void RespawnPlayer()
    {
        transform.position = checkpointPosition;
        rb.linearVelocity = Vector2.zero;
    }


    public void CollectFruit()
    {
        fruitCount++; // Incrémente le compteur
        Debug.Log("Fruits collectés : " + fruitCount);
    }
}
