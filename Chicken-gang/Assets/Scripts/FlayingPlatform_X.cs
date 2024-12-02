using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de d�placement de la plateforme
    public float moveDistance = 5f; // Distance maximale � parcourir sur l'axe X
    private Vector3 startPosition; // Position de d�part de la plateforme
    private bool movingRight = true; // D�termine la direction du mouvement

    private void Start()
    {
        startPosition = transform.position; // Enregistre la position initiale de la plateforme
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        // D�placement de la plateforme
        if (movingRight)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            if (transform.position.x >= startPosition.x + moveDistance) // Si la plateforme a atteint la limite droite
            {
                movingRight = false; // Inverse la direction
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            if (transform.position.x <= startPosition.x - moveDistance) // Si la plateforme a atteint la limite gauche
            {
                movingRight = true; // Inverse la direction
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Si le joueur est sur la plateforme, on d�place le joueur avec elle
        if (collision.gameObject.CompareTag("Player"))
        {
            // D�place le joueur en synchronisation avec la plateforme sur l'axe X
            collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, collision.transform.position.z);
        }
    }
}
