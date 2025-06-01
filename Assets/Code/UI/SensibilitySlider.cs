using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [Header("Settings")]
    [SerializeField] private float min;
    [SerializeField] private float max;

    private void Start()
    {
        slider.value = SensToValue(MicrophoneInput.Instance.Sensibility);
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
        MicrophoneInput.Instance.Sensibility = ValueToSens(value);
    }

    float SensToValue(float sensibility) => (sensibility - min) / (max - min);
    float ValueToSens(float value) => min + (max - min) * value;
}
