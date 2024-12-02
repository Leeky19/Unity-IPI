using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Charge le niveau suivant via le LevelManager
            FindAnyObjectByType<LevelManager>().LoadNextLevel();
        }
    }
}
