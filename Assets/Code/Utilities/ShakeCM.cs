using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeAmplitude = 2f;
    [SerializeField] private float shakeFrequency = 2f;

    private float shakeTimer;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        if (noise != null)
        {
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0f;
        }
    }
    private void Awake()
    {
        Instance = this;
        noise = virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                StopShake();
            }
        }
    }

    public void Shake()
    {
        noise.AmplitudeGain = shakeAmplitude;
        noise.FrequencyGain = shakeFrequency;
        shakeTimer = shakeDuration;
    }

    private void StopShake()
    {
        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}
