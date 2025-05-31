using TMPro;
using UnityEngine;

public class DeviceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update() => Text();

    private void Text()
    {
        if(text == null)
        {
            return;
        }

        text.text = MicrophoneInput.Instance.device.name;
    }
}
