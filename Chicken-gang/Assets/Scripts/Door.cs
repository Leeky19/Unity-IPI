using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform upperPart;        // Partie supérieure de la porte
    public Transform lowerPart;        // Partie inférieure de la porte
    public float movementDistance = 2f; // Distance maximale de déplacement
    public float movementSpeed = 2f;    // Vitesse de déplacement
    public PlayerController player;    // Référence au script du joueur

    private bool isOpening = false;     // Indique si la porte est en train de s'ouvrir
    private float initialUpperY;        // Position initiale de la partie supérieure
    private float initialLowerY;        // Position initiale de la partie inférieure

    private Collider2D[] colliders;     // Liste des colliders associés à la porte

    void Start()
    {
        // Stocke les positions initiales
        if (upperPart != null) initialUpperY = upperPart.position.y;
        if (lowerPart != null) initialLowerY = lowerPart.position.y;

        // Récupère tous les colliders attachés à la porte
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        if (!isOpening && player != null && player.fruitCount >= 1)
        {
            isOpening = true;
        }

        if (isOpening)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        // Déplace la partie supérieure vers le haut
        if (upperPart != null && upperPart.position.y < initialUpperY + movementDistance)
        {
            upperPart.position += Vector3.up * movementSpeed * Time.deltaTime;
        }

        // Déplace la partie inférieure vers le bas
        if (lowerPart != null && lowerPart.position.y > initialLowerY - movementDistance)
        {
            lowerPart.position += Vector3.down * movementSpeed * Time.deltaTime;
        }

        // Une fois que les deux parties ont atteint leur destination
        if (upperPart.position.y >= initialUpperY + movementDistance &&
            lowerPart.position.y <= initialLowerY - movementDistance)
        {
            isOpening = false;
            Debug.Log("Porte complètement ouverte !");
            DisableColliders(); // Désactive les colliders
        }
    }

    private void DisableColliders()
    {
        Destroy(gameObject); // Supprime la porte
    }
}
