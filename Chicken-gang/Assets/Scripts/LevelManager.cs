using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string LevelKey = "CurrentLevel"; // Cl� pour sauvegarder le niveau dans PlayerPrefs

    void Start()
    {
        // Charger le niveau sauvegard� au d�but du jeu
        int savedLevelIndex = PlayerPrefs.GetInt(LevelKey, 0); // Par d�faut, commence au niveau 0
        if (SceneManager.GetActiveScene().buildIndex != savedLevelIndex)
        {
            SceneManager.LoadScene(savedLevelIndex);
        }
    }

    public void LoadNextLevel()
    {
        // R�cup�re l'index de la sc�ne actuelle
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Passe � la sc�ne suivante (si elle existe)
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
            // Optionnel : revenir au menu principal ou une autre sc�ne
            // SceneManager.LoadScene("MainMenu");
        }
    }

    public void RestartLevel()
    {
        // Recharge la sc�ne actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetProgress()
    {
        // R�initialise la progression du joueur
        PlayerPrefs.DeleteKey(LevelKey);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0); // Repart du premier niveau
    }
}
