using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void RetourAuMenu()
{
    PlayerPrefs.DeleteKey("CurrentLevel");
    PlayerPrefs.Save();
    SceneManager.LoadScene("MenuPrincipal");
}

}
