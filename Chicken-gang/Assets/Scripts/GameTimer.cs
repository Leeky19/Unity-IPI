using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance; // Singleton pour accéder au timer globalement
    private float timer = 0f;
    private bool isTiming = false;

    void Awake()
    {
        // Créer un singleton pour persister entre les scènes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Incrémente le timer si activé
        if (isTiming)
        {
            timer += Time.deltaTime;
        }

        // Arrête le chrono lorsque la scène "Credits" est atteinte
        if (SceneManager.GetActiveScene().name == "Credits" && isTiming)
        {
            isTiming = false;
            Debug.Log("Temps total : " + timer + " secondes");
        }
    }

    // Méthode pour démarrer le chrono
    public void StartTimer()
    {
        timer = 0f;
        isTiming = true;
        Debug.Log("Chrono démarré !");
    }

    // Méthode pour récupérer le temps écoulé
    public float GetTime()
    {
        return timer;
    }
}
