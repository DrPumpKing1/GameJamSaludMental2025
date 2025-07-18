using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlModeSelector : MonoBehaviour
{
    [SerializeField] private GameObject controlSelectorCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Button micButton;
    [SerializeField] private Button tapButton;

    private static bool hasShownControlSelector = false;

    private void Start()
    {
        if (!hasShownControlSelector)
        {
            hasShownControlSelector = true;
            controlSelectorCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
        }
        else
        {
            controlSelectorCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }

        micButton.onClick.AddListener(() => SelectControlMode(true));
        tapButton.onClick.AddListener(() => SelectControlMode(false));
    }

    private void SelectControlMode(bool useMic)
    {
        PlayerPrefs.SetInt("UseMicrophone", useMic ? 1 : 0);
        PlayerPrefs.Save();

        controlSelectorCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
