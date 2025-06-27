using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    private const float defaultSensibility = .5f;
    private const string SensKey = "MicSensibility";
    [SerializeField] private Slider slider;

    [Header("Settings")]
    [SerializeField] private float min;
    [SerializeField] private float max;

    private void Start()
    {
        float defaultSens = defaultSensibility;
        if (MicrophoneInput.Instance != null) defaultSens = defaultSensibility;
        float saveSens = PlayerPrefs.GetFloat(SensKey, defaultSens);
        slider.value = SensToValue(saveSens);
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float value)
    {
        float saveSens = ValueToSens(value);
        PlayerPrefs.SetFloat(SensKey, saveSens);
        if(MicrophoneInput.Instance != null) MicrophoneInput.Instance.Sensibility = saveSens;
    }

    float SensToValue(float sensibility) => (sensibility - min) / (max - min);
    float ValueToSens(float value) => min + (max - min) * value;
}
