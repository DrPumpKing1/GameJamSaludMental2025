using TMPro;
using UnityEngine;

public class InputStateText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    void Update() => Text();

    private void Text()
    {
        if(text == null)
        {
            return;
        }

        text.text = MicrophoneInputProcessor.Instance.lastInputState.ToString();
    }
}
