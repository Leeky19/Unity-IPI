using UnityEngine;

public class Fruit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Trouve le PlayerController sur le joueur et incrémente le compteur
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.CollectFruit(); // Appelle la méthode dans PlayerController
            }
            Destroy(gameObject); // Supprime le fruit après collecte
        }
    }
}