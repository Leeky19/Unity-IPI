using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform upperPart;        // Partie sup�rieure de la porte
    public Transform lowerPart;        // Partie inf�rieure de la porte
    public float movementDistance = 2f; // Distance maximale de d�placement
    public float movementSpeed = 2f;    // Vitesse de d�placement
    public PlayerController player;    // R�f�rence au script du joueur

    private bool isOpening = false;     // Indique si la porte est en train de s'ouvrir
    private float initialUpperY;        // Position initiale de la partie sup�rieure
    private float initialLowerY;        // Position initiale de la partie inf�rieure

    private Collider2D[] colliders;     // Liste des colliders associ�s � la porte

    void Start()
    {
        // Stocke les positions initiales
        if (upperPart != null) initialUpperY = upperPart.position.y;
        if (lowerPart != null) initialLowerY = lowerPart.position.y;

        // R�cup�re tous les colliders attach�s � la porte
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
        // D�place la partie sup�rieure vers le haut
        if (upperPart != null && upperPart.position.y < initialUpperY + movementDistance)
        {
            upperPart.position += Vector3.up * movementSpeed * Time.deltaTime;
        }

        // D�place la partie inf�rieure vers le bas
        if (lowerPart != null && lowerPart.position.y > initialLowerY - movementDistance)
        {
            lowerPart.position += Vector3.down * movementSpeed * Time.deltaTime;
        }

        // Une fois que les deux parties ont atteint leur destination
        if (upperPart.position.y >= initialUpperY + movementDistance &&
            lowerPart.position.y <= initialLowerY - movementDistance)
        {
            isOpening = false;
            Debug.Log("Porte compl�tement ouverte !");
            DisableColliders(); // D�sactive les colliders
        }
    }

    private void DisableColliders()
    {
        Destroy(gameObject); // Supprime la porte
    }
}
