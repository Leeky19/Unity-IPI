using UnityEngine;
using TMPro;

public class CreditsDisplay : MonoBehaviour
{
    public TextMeshProUGUI chronoText;

    void Start()
    {
        // Récupère le temps total depuis le GameTimer
        float totalTime = GameTimer.Instance.GetTime();
        chronoText.text = "Temps total : " + totalTime.ToString("F2") + " secondes";
    }
}
