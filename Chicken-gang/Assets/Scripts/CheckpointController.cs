using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Animator checkpointAnimator; // Référence à l'Animator du checkpoint
    private bool isActivated = false;   // Indique si le checkpoint a déjà été activé

    void Start()
    {
        // Récupère l'Animator attaché au checkpoint
        checkpointAnimator = GetComponent<Animator>();
        if (checkpointAnimator == null)
        {
            Debug.LogError("Aucun Animator trouvé sur le Checkpoint !");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated) // Vérifie que c'est le joueur et que le checkpoint n'est pas encore activé
        {
            isActivated = true; // Empêche de réactiver le checkpoint
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger("appear"); // Déclenche l'animation du checkpoint
            }
            Debug.Log("Checkpoint activé !");
        }
    }
}
