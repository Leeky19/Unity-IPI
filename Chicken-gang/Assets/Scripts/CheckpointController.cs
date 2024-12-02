using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Animator checkpointAnimator; // Référence à l'Animator
    private bool isActivated = false;   // Évite les activations multiples

    void Start()
    {
        // Initialisation de l'Animator
        checkpointAnimator = GetComponent<Animator>();
        if (checkpointAnimator == null)
        {
            Debug.LogError("Animator non trouvé sur ce checkpoint !");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated) // Vérifie que c'est le joueur
        {
            isActivated = true; // Empêche les réactivations multiples

            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger("appear");
                Debug.Log("Checkpoint activé avec succès.");
            }
            else
            {
                Debug.LogError("Impossible de déclencher l'animation : Animator manquant !");
            }
        }
    }
}
