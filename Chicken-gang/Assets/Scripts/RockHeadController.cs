using UnityEngine;
using System.Collections;

public class RockHeadController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition; // Position initiale du Rock_Head
    private bool isFalling = true; // Contrôleur de chute
    private bool isRising = false; // Contrôleur de montée
    private bool isPaused = false; // Contrôleur de pause
    public float fallSpeed = 5f; // Vitesse de la chute
    public float riseSpeed = 3f; // Vitesse de la montée
    public float groundPauseTime = 1f; // Temps de pause au sol avant de monter

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D mainCollider; // Référence au collider principal
    [SerializeField] private BoxCollider2D triggerCollider; // Référence au collider avec "Is Trigger"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D attaché à Rock_Head
        if (mainCollider == null || triggerCollider == null)
        {
            Debug.LogError("Veuillez assigner les colliders dans l'inspecteur !");
        }

        initialPosition = transform.position; // Enregistre la position initiale
        triggerCollider.enabled = false; // Désactive le collider trigger au départ
    }

    void Update()
    {
        if (isFalling)
        {
            // Mouvement de chute
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fallSpeed); // Applique une vitesse verticale négative (chute)
            triggerCollider.enabled = true; // Active le collider trigger pendant la chute
        }
        else if (isRising)
        {
            // Mouvement de montée
            if (transform.position.y < initialPosition.y) // Vérifie si le Rock_Head n'a pas atteint sa position initiale
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, riseSpeed); // Applique une vitesse verticale positive (montée)
            }
            else
            {
                rb.linearVelocity = Vector2.zero; // Arrête le mouvement une fois que la position initiale est atteinte
                transform.position = initialPosition; // Réinitialise la position exactement à celle de départ
                isFalling = true; // Le Rock_Head peut recommencer à tomber après avoir atteint la position initiale
                isRising = false; // Arrête le mouvement de montée
            }
            triggerCollider.enabled = false; // Désactive le collider trigger pendant la montée
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isPaused)
        {
            // Lorsque le Rock_Head touche le sol
            Debug.Log("Rock_Head a touché le sol!");

            isFalling = false; // Le Rock_Head doit commencer à monter
            isRising = false; // Arrête la montée
            isPaused = true; // Démarre la pause

            StartCoroutine(PauseBeforeRising()); // Démarre la coroutine pour gérer la pause
        }
    }

    private IEnumerator PauseBeforeRising()
    {
        // Attend pendant le temps de pause avant de remonter
        yield return new WaitForSeconds(groundPauseTime);

        isPaused = false; // Fin de la pause
        isRising = true; // Commence la montée
    }
}
