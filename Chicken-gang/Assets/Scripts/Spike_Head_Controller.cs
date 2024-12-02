using UnityEngine;
using System.Collections;

public class SpikeHeadController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 initialPosition; // Position initiale du Spike_Head
    private bool isFalling = true; // Contr�leur de chute
    private bool isRising = false; // Contr�leur de mont�e
    private bool isPaused = false; // Contr�leur de pause
    public float fallSpeed = 5f; // Vitesse de la chute
    public float riseSpeed = 3f; // Vitesse de la mont�e
    public float groundPauseTime = 1f; // Temps de pause au sol avant de monter

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D mainCollider; // R�f�rence au collider principal
    [SerializeField] private PolygonCollider2D triggerCollider; // R�f�rence au collider avec "Is Trigger"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le Rigidbody2D attach� � Spike_Head
        if (mainCollider == null || triggerCollider == null)
        {
            Debug.LogError("Veuillez assigner les colliders dans l'inspecteur !");
            enabled = false; // D�sactive le script si les colliders ne sont pas assign�s
        }

        initialPosition = transform.position; // Enregistre la position initiale
        triggerCollider.isTrigger = true; // S'assure que le collider trigger est activ� en permanence
    }

    void Update()
    {
        if (isFalling)
        {
            // Mouvement de chute
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -fallSpeed); // Applique une vitesse verticale n�gative (chute)
        }
        else if (isRising)
        {
            // Mouvement de mont�e
            if (transform.position.y < initialPosition.y) // V�rifie si le Spike_Head n'a pas atteint sa position initiale
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, riseSpeed); // Applique une vitesse verticale positive (mont�e)
            }
            else
            {
                rb.linearVelocity = Vector2.zero; // Arr�te le mouvement une fois que la position initiale est atteinte
                transform.position = initialPosition; // R�initialise la position exactement � celle de d�part
                isFalling = true; // Le Spike_Head peut recommencer � tomber apr�s avoir atteint la position initiale
                isRising = false; // Arr�te le mouvement de mont�e
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isPaused)
        {
            // Lorsque le Spike_Head touche le sol
            Debug.Log("Spike_Head a touch� le sol!");

            isFalling = false; // Le Spike_Head doit commencer � monter
            isRising = false; // Arr�te la mont�e
            isPaused = true; // D�marre la pause

            StartCoroutine(PauseBeforeRising()); // D�marre la coroutine pour g�rer la pause
        }
    }

    private IEnumerator PauseBeforeRising()
    {
        // Attend pendant le temps de pause avant de remonter
        yield return new WaitForSeconds(groundPauseTime);

        isPaused = false; // Fin de la pause
        isRising = true; // Commence la mont�e
    }
}
