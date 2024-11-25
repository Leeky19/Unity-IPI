using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Animator checkpointAnimator;  // R�f�rence � l'Animator du checkpoint

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpointAnimator.SetTrigger("Monter");  // D�clenche l'animation du drapeau
        }
    }
}
