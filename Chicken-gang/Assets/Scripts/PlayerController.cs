using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Vitesse de d�placement
    public float jumpForce = 10f; // Force de saut

    private Rigidbody2D rb; // R�f�rence au Rigidbody2D
    private bool isGrounded; // V�rifie si le joueur est au sol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le Rigidbody2D attach� au player
    }

    void Update()
    {
        MovePlayer(); // Appelle la fonction de d�placement
        if (isGrounded && Input.GetButtonDown("Jump")) // Si au sol et appuie sur "Espace"
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float move = Input.GetAxis("Horizontal"); // D�placement horizontal
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y); // Applique le d�placement horizontal tout en maintenant la v�locit� verticale
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Applique la force de saut
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
