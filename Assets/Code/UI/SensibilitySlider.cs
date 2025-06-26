using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    private const string SensKey = "MicSensibility";
    [SerializeField] private Slider slider;

    [Header("Settings")]
    [SerializeField] private float min;
    [SerializeField] private float max;

    private void Start()
    {
        float saveSens = PlayerPrefs.GetFloat(SensKey, MicrophoneInput.Instance.Sensibility);
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
        MicrophoneInput.Instance.Sensibility = saveSens;
    }

    float SensToValue(float sensibility) => (sensibility - min) / (max - min);
    float ValueToSens(float value) => min + (max - min) * value;
}
