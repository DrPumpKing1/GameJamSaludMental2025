using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string escenaNombre;
    [SerializeField] private bool usarFade = false;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float fadeInDuration;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color color = fadeImage.color;
            color.a = 1; // Comenzamos con opacidad completa
            fadeImage.color = color;
            StartCoroutine(FadeOutAtStart());
        }
    }

    private IEnumerator FadeOutAtStart()
    {
        float timer = 0;
        Color color = fadeImage.color;

        while (timer < fadeOutDuration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeOutDuration);
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // Oculta la imagen luego del fade out
    }

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
        fadeImage.gameObject.SetActive(true);
        float timer = 0;
        Color color = fadeImage.color;

        while (timer < fadeInDuration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeInDuration);
            fadeImage.color = color;
            yield return null;
        }

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
