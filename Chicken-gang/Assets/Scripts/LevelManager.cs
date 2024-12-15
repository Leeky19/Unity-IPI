using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string LevelKey = "CurrentLevel"; // Cl� pour sauvegarder le niveau dans PlayerPrefs

   void Start()
{
    int savedLevelIndex = PlayerPrefs.GetInt(LevelKey, 1);
    Debug.Log("Niveau sauvegardé : " + savedLevelIndex);
    Debug.Log("Niveau actif : " + SceneManager.GetActiveScene().buildIndex);

    // Si on est dans le menu principal, ne rien faire
    if (SceneManager.GetActiveScene().name == "MenuPrincipal")
    {
        return;
    }

    // Ne recharger le niveau sauvegardé que si on est dans une scène de jeu
    if (SceneManager.GetActiveScene().buildIndex != savedLevelIndex)
    {
        Debug.Log("Chargement du niveau sauvegardé : " + savedLevelIndex);
        SceneManager.LoadScene(savedLevelIndex);
    }
}



   public void LoadNextLevel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // Passe à la scène suivante (si elle existe)
    if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
    {
        int nextSceneIndex = currentSceneIndex + 1;

        // Ne sauvegarder que si ce n'est pas le menu principal ou les crédits
        string nextSceneName = SceneManager.GetSceneByBuildIndex(nextSceneIndex).name;
        if (nextSceneName != "MenuPrincipal" && nextSceneName != "Credits")
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

    public void RetourMenuPrincipal()
{
    PlayerPrefs.DeleteKey("CurrentLevel");
    SceneManager.LoadScene("MenuPrincipal");
}

}
