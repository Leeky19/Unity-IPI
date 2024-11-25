using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Animator checkpointAnimator;  // Référence à l'Animator du checkpoint

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpointAnimator.SetTrigger("Monter");  // Déclenche l'animation du drapeau
        }
    }
}
