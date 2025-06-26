using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MicrophoneInputProcessor : Singleton<MicrophoneInputProcessor>
{
    private const int bufferSize = 1024;

    [Header("Processing Settings")]
    [SerializeField] private float sampleRate = 60;
    private float timeBetweenTicks;
    private float tickTimer;
    private int tick;
    [SerializeField] private List<InputRange> inputRanges = new();
    private CircularBuffer<InputStateData> tickStateBuffer;
    public InputRange lastTickInputState { get; private set; } = NullRange.Get;
    public float lastTickloudness { get; private set; }

    [Header("Round Processing Settings")]
    [SerializeField] private int roundWindow = 16;
    [SerializeField] private int roundTickRate = 1;
    private int round;
    
    public InputRange lastRoundInputState { get; private set; } = NullRange.Get;
    public float lastRoundloudness { get; private set; }


    protected override void Awake()
    {
        tickStateBuffer = new (bufferSize);
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
        lastTickInputState = NullRange.Get;

        if(!MicrophoneInput.Instance.isRecording)
        {
            return;
        }

        float loudnessPreAmp = MicrophoneInput.Instance.loudnessPreAmp;
        float loudness = MicrophoneInput.Instance.loudness;
        lastTickloudness = loudness;

        for (int i = 0; i < inputRanges.Count; i++)
        {
            var range = inputRanges[i];
            if(range.InRange(loudnessPreAmp))
            {
                lastTickInputState = range;
                break;
            }
        }

        tickStateBuffer.Add(new(lastTickInputState.level, loudness), tick);
        tick++;
        if (tick % roundTickRate == 0) Round();
    }

    private void Round()
    {
        var counter = new Dictionary<int, int>();
        foreach(var range in inputRanges)
        {
            counter.Add(range.level, 0);
        }

        int frequent = -1;
        int frequency = 0;
        for(int i = 0; i < roundWindow; i++)
        {
            var replay = tick - i;

            if (replay < 0) break;

            var level = tickStateBuffer.Get(replay).level;
            if (level == -1) continue;
            counter[level]++;

            if (counter[level] > frequency)
            {
                frequent = level;
                frequency = counter[level];
            }
        }

        lastRoundInputState = GetInputRangeByLevel(frequent);
        lastRoundloudness = lastTickloudness;
    }

    private InputRange GetInputRangeByLevel(int level)
    {
        var found = inputRanges.FirstOrDefault(range => range.level == level);
        if (found == default) return NullRange.Get;
        return found;
    }
}

[Serializable]
public struct InputRange
{
    public string name;
    public int level;
    public float parameter;
    [Space, Range(0, 1)] public float lower;
    [Range(0, 1)] public float upper;

    public InputRange(int level, float parameter, string name, float lower = 0, float upper = 0)
    {
        this.level = level;
        this.parameter = parameter;
        this.name = name;
        this.lower = lower;
        this.upper = upper;
    }

    public bool InRange(float value) => value >= lower && value < upper;

    public bool Equals(InputRange other)
    {
        return level == other.level;
    }

    public override bool Equals(object obj)
    {
        return obj is InputRange other && Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(InputRange lhs, InputRange rhs) => lhs.Equals(rhs);
    public static bool operator !=(InputRange lhs, InputRange rhs) => !(lhs == rhs);
}

public static class NullRange
{
    public static InputRange Get { get; } = new(-1, 0, "Silence");
}

public struct InputStateData
{
    public int level;
    public float loudness;

    public InputStateData(int level, float loudness)
    {
        this.level = level;
        this.loudness = loudness;
    }
}
