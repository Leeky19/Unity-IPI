using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalController : MonoBehaviour
{
    // Méthode pour démarrer une partie
    public void Jouer()
    {
         // Lancer le chrono
        GameTimer.Instance.StartTimer();
        Debug.Log("Bouton Jouer cliqué, chargement de la scène Start");
        SceneManager.LoadScene("Start");
    }
}