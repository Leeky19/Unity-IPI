using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance; // Singleton
    private float timer = 0f;
    private bool isTiming = false;
    private const string BestTimeKey = "BestTime"; // Clé pour sauvegarder le meilleur temps

    void Awake()
    {
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
        if (isTiming)
        {
            timer += Time.deltaTime;
        }

        // Arrêter le chrono dans la scène "Credits"
        if (SceneManager.GetActiveScene().name == "Credits" && isTiming)
        {
            isTiming = false;
            SaveBestTime(timer);
            Debug.Log("Temps total : " + timer + " secondes");
        }
    }

    // Démarrer le chrono
    public void StartTimer()
    {
        timer = 0f;
        isTiming = true;
        Debug.Log("Chrono démarré !");
    }

    // Récupérer le temps total
    public float GetTime()
    {
        return timer;
    }

    // Sauvegarder le meilleur temps si c'est le nouveau record
    private void SaveBestTime(float currentTime)
{
    float bestTime = PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);
    if (currentTime < bestTime)
    {
        Debug.Log("Nouveau record ! Ancien meilleur temps : " + bestTime);
        PlayerPrefs.SetFloat(BestTimeKey, currentTime);
        PlayerPrefs.Save();
    }
    else
    {
        Debug.Log("Pas de nouveau record. Meilleur temps actuel : " + bestTime);
    }
}


    // Récupérer le meilleur temps
    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue); // Valeur par défaut = max possible
    }
}
