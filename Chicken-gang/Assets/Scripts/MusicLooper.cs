using UnityEngine;
using System.Collections;

public class MusicLooper : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Settings")]
    public float fadeDuration = 2f; // Durée du fondu (en secondes)
    public float trackLength = 7f; // Durée totale de la piste (en secondes)

    void Start()
    {
        // Initialiser l'AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Désactive la boucle automatique
        audioSource.volume = 0.01f; // Commence à volume 0

        // Lancer la musique avec un fondu entrant
        StartCoroutine(PlayMusicWithFade());
    }

    private IEnumerator PlayMusicWithFade()
    {
        while (true) // Boucle infinie pour rejouer la piste
        {
            // Jouer la piste avec un fondu entrant
            audioSource.Play();
            yield return StartCoroutine(FadeAudio(0.02f, 0.02f, fadeDuration)); // Fondu entrant

            // Attendre la durée de la piste moins la durée du fondu sortant
            yield return new WaitForSeconds(trackLength - (2 * fadeDuration));

            // Appliquer un fondu sortant
            yield return StartCoroutine(FadeAudio(0.02f, 0f, fadeDuration)); // Fondu sortant

            // Arrêter la musique pour préparer la prochaine boucle
            audioSource.Stop();
        }
    }

    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float currentTime = 0.2f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // S'assure que le volume atteint la cible
    }
}
