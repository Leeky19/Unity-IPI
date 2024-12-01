using UnityEngine;

public class RockHeadController : MonoBehaviour
{
    public float fallSpeed = 5f; // Vitesse de chute
    public float riseSpeed = 2f; // Vitesse de remont�e
    private Vector3 initialPosition; // Position initiale du Rock_Head
    private bool isFalling = true; // Permet de g�rer l'�tat de chute et de remont�e
    private Rigidbody2D rb; // R�f�rence au Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le Rigidbody2D attach� au GameObject
        initialPosition = transform.position; // Enregistre la position initiale
    }

    void Update()
    {
        if (isFalling)
        {
            // Applique la force pour la chute
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Vitesse de chute vers le bas
        }
        else
        {
            // Remonte � la position initiale apr�s avoir touch� le sol
            float distanceToMove = Vector3.Distance(transform.position, initialPosition);
            if (distanceToMove > 0.1f) // Si le Rock_Head n'est pas d�j� � sa position initiale
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, riseSpeed * Time.deltaTime); // Remont�e � la position initiale
            }
            else
            {
                // Une fois que le Rock_Head est de retour � sa position initiale, il recommence � tomber
                isFalling = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Lorsque le Rock_Head touche le sol, il arr�te de tomber et commence � remonter
        if (collision.gameObject.CompareTag("Ground")) // Assure-toi que ton sol a le tag "Ground"
        {
            isFalling = false; // Arr�te la chute
        }
    }
}
