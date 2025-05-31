using UnityEngine;

public class ScaleWithLoudness : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float threshold;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update() => Scale();

    private void Scale()
    {
        float loudness = MicrophoneInput.Instance.loudness;
        if (loudness < threshold)
            return;

        transform.localScale = initialScale * (1 + loudness);
    }
}
