using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string LevelKey = "CurrentLevel"; // Cl� pour sauvegarder le niveau dans PlayerPrefs

   void Start()
{
    int savedLevelIndex = PlayerPrefs.GetInt(LevelKey, 0);
    Debug.Log("Niveau sauvegardé : " + savedLevelIndex);
    Debug.Log("Niveau actif : " + SceneManager.GetActiveScene().buildIndex);

    if (SceneManager.GetActiveScene().name != "MenuPrincipal")
    {
        if (SceneManager.GetActiveScene().buildIndex != savedLevelIndex)
        {
            Debug.Log("Chargement du niveau sauvegardé : " + savedLevelIndex);
            SceneManager.LoadScene(savedLevelIndex);
        }
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

        // Sauvegarde le prochain niveau uniquement si ce n'est pas le menu principal
        if (SceneManager.GetSceneByBuildIndex(nextSceneIndex).name != "MenuPrincipal")
        {
            PlayerPrefs.SetInt(LevelKey, nextSceneIndex);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
        // Si aucun niveau suivant n'existe, revenir au menu principal
        SceneManager.LoadScene("MenuPrincipal");
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
        SceneManager.LoadScene(1); // Repart du premier niveau
    }
}
