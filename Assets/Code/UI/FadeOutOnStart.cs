using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutOnStart : MonoBehaviour
{
    [Tooltip("Asigna la imagen del Canvas que se usará para el efecto de fade.")]
    [SerializeField] private Image fadeImage;

    [Tooltip("Duración del efecto de Fade Out al iniciar la escena.")]
    [SerializeField] private float fadeOutDuration = 1.5f; // Duración por defecto de 1.5 segundos

    private void Start()
    {
        // Asegúrate de que hay una imagen asignada para el fade
        if (fadeImage == null)
        {
            Debug.LogWarning("Fade Image no asignada en FadeOutOnStart. El efecto de fade no se ejecutará.");
            return;
        }

        // Activa la imagen y pon su opacidad al máximo (completamente visible)
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;

        // Inicia la coroutine para el efecto de fade out
        StartCoroutine(DoFadeOut());
    }

    private IEnumerator DoFadeOut()
    {
        float timer = 0;
        Color color = fadeImage.color;

        // Bucle para reducir la opacidad gradualmente
        while (timer < fadeOutDuration)
        {
            timer += Time.unscaledDeltaTime; // Usa Time.unscaledDeltaTime para que no le afecte la pausa del juego
            color.a = Mathf.Lerp(1, 0, timer / fadeOutDuration); // Interpola de opaco (1) a transparente (0)
            fadeImage.color = color;
            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que la opacidad final es 0 y desactiva la imagen al terminar
        color.a = 0;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
}
