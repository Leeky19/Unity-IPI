using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f; // Vitesse de défilement
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Déplacer le texte vers le haut
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
