using Code.Networking.ClientPrediction;
using System;
using UnityEngine;

public class MicrophoneInputProcessor : Singleton<MicrophoneInputProcessor>
{
    private const int bufferSize = 1024;

    [Header("Processing Settings")]
    [SerializeField] private float sampleRate = 60;
    private float timeBetweenTicks;
    private float tickTimer;
    [SerializeField] private Range lowState;
    [SerializeField] private Range highState;
    private CircularBuffer<int> tickStateBuffer; 
    public MicrophoneInputStates lastInputState { get; private set; }
    public bool IsHigh => lastInputState == MicrophoneInputStates.High;
    public bool IsLow => lastInputState == MicrophoneInputStates.Low;
    public bool IsSilence => lastInputState == MicrophoneInputStates.Silence;

    [Header("Runtime")]
    [SerializeField] private int tick;

    protected override void Awake()
    {
        tickStateBuffer = new CircularBuffer<int>(bufferSize);
        timeBetweenTicks = 1 / sampleRate;
    }

    void Update()
    {
        if (tickTimer > 0) tickTimer -= Time.deltaTime;
        else
        {
            tickTimer = timeBetweenTicks;
            Tick();
        }
    }

    private void Tick()
    {
        float loudness = MicrophoneInput.Instance.loudness;

        if(lowState.InRange(loudness))
        {
            lastInputState = MicrophoneInputStates.Low;
        } else if(highState.InRange(loudness))
        {
            lastInputState = MicrophoneInputStates.High;
        } else
        {
            lastInputState = MicrophoneInputStates.Silence;
        }

        tickStateBuffer.Add((int)lastInputState, tick);
        tick++;
    }
}

[Serializable]
public struct Range
{
    [Range(0, 1)] public float lower;
    [Range(0, 1)] public float upper;

    public bool InRange(float value) => value >= lower && value <= upper;
}

public enum MicrophoneInputStates
{
    Low,
    High,
    Silence
}
