using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string LevelKey = "CurrentLevel"; // Clé pour sauvegarder le niveau dans PlayerPrefs

    void Start()
    {
        // Charger le niveau sauvegardé au début du jeu
        int savedLevelIndex = PlayerPrefs.GetInt(LevelKey, 0); // Par défaut, commence au niveau 0
        if (SceneManager.GetActiveScene().buildIndex != savedLevelIndex)
        {
            SceneManager.LoadScene(savedLevelIndex);
        }
    }

    public void LoadNextLevel()
    {
        // Récupère l'index de la scène actuelle
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Passe à la scène suivante (si elle existe)
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            PlayerPrefs.SetInt(LevelKey, nextSceneIndex); // Sauvegarde le prochain niveau
            PlayerPrefs.Save();

            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            int nextSceneIndex = 0;
            PlayerPrefs.SetInt(LevelKey, nextSceneIndex); // Sauvegarde le prochain niveau
            PlayerPrefs.Save();

            SceneManager.LoadScene(nextSceneIndex);
            // Optionnel : revenir au menu principal ou une autre scène
            // SceneManager.LoadScene("MainMenu");
        }
    }

    public void RestartLevel()
    {
        // Recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetProgress()
    {
        // Réinitialise la progression du joueur
        PlayerPrefs.DeleteKey(LevelKey);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0); // Repart du premier niveau
    }
}
