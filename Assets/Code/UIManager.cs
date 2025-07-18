using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject configPanel;
    private static bool configPanelAlreadyShown = false;

    void Start()
    {
        if (!configPanelAlreadyShown)
        {
            configPanel.SetActive(true);
            configPanelAlreadyShown = true;
        }
        else
        {
            configPanel.SetActive(false);
        }
    }

    public void HideConfigPanel()
    {
        configPanel.SetActive(false);
    }
}
