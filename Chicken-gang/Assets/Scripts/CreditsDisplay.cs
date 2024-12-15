using UnityEngine;
using TMPro;

public class CreditsDisplay : MonoBehaviour
{
    public TextMeshProUGUI chronoText; // Texte pour le temps total
    public TextMeshProUGUI bestTimeText; // Texte pour le meilleur temps

   void Start()
{
    if (GameTimer.Instance != null)
    {
        float totalTime = GameTimer.Instance.GetTime();
        float bestTime = GameTimer.Instance.GetBestTime();

        Debug.Log("Temps total : " + totalTime);
        Debug.Log("Meilleur temps : " + bestTime);

        chronoText.text = FormatTime("Temps total : ", totalTime);

        if (bestTime < float.MaxValue)
        {
            bestTimeText.text = FormatTime("Meilleur temps : ", bestTime);
        }
        else
        {
            bestTimeText.text = "Meilleur temps : Aucun record";
        }
    }
    else
    {
        Debug.LogError("GameTimer.Instance est nul !");
        chronoText.text = "Temps indisponible.";
        bestTimeText.text = "Meilleur temps : Aucun record";
    }
}


    private string FormatTime(string label, float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{label}{minutes}m {seconds}s";
    }
}
