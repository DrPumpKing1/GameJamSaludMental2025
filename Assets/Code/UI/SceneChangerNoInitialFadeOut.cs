using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangerNoInitialFadeOut : MonoBehaviour
{
    [SerializeField] private string escenaNombre;
    [SerializeField] private bool usarFade = false;
    [SerializeField] private Image fadeImage;
    // [SerializeField] private float fadeOutDuration; // Eliminada ya que no se usará un fadeOut al inicio
    [SerializeField] private float fadeInDuration;

    // No se hace nada en Start() relacionado con el fade, la imagen debe estar inicialmente desactivada
    private void Start()
    {
        // Asegúrate de que la imagen de fade esté inicialmente desactivada en el Inspector de Unity
        // Ocultar aquí para mayor seguridad si no la desactivaste manualmente.
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    // El método FadeOutAtStart ha sido eliminado por completo.

    public void ChangeScene()
    {
        if (usarFade && fadeImage != null)
        {
            StartCoroutine(FadeInAndChangeScene());
        }
        else
        {
            SceneManager.LoadScene(escenaNombre);
        }
    }

    private IEnumerator FadeInAndChangeScene()
    {
        // Activa la imagen del fade para iniciar el FadeIn
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            // Asegúrate de que la imagen de fade empiece completamente transparente (alpha = 0)
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
        }


        float timer = 0;
        Color currentColor = fadeImage.color; // Usamos un nombre de variable diferente para evitar confusión

        while (timer < fadeInDuration)
        {
            timer += Time.unscaledDeltaTime;
            // Interpola de transparente (0) a opaco (1)
            currentColor.a = Mathf.Lerp(0, 1, timer / fadeInDuration);
            fadeImage.color = currentColor;
            yield return null;
        }

        // Asegura que la opacidad final sea 1 (completamente opaco) antes de cargar la escena
        currentColor.a = 1;
        fadeImage.color = currentColor;

        SceneManager.LoadScene(escenaNombre);
    }

    #region Editor Support Methods

    public string GetSceneName()
    {
        return escenaNombre;
    }

    public void SetSceneName(string sceneName)
    {
        escenaNombre = sceneName;
    }

    #endregion
}
