using TMPro;
using UnityEngine;

public class LoudnessGauge : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update() => Text();

    private void Text()
    {
        if (text == null) return;

        text.text = MicrophoneInput.Instance.loudness.ToString("#.00");
    }
}
