using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class MicrophoneInput : Singleton<MicrophoneInput>
{
    public List<MicrophoneDevice> devices { get; } = new();
    public MicrophoneDevice device { get; private set; }
    private AudioClip clip;
    public float loudness { get; private set; }
    public bool isRecording { get; private set; }
    public event Action OnDeviceListChanged;

    [Header("Recording Settings")]
    [SerializeField] private int sampleWindow = 64;
    [SerializeField] private int minSamples = 32;

    protected override void Awake()
    {
        LoadMicrophones();
    }

    private void Start()
    {
        StartRecording();
    }

    private void Update()
    {
        HandleDeviceListChanged();
        UpdateLoudnessFromMicrophone();
    }

    private void LoadMicrophones()
    {
        if (devices.Count > 0) devices.Clear();

        foreach(var device in Microphone.devices)
        {
            devices.Add(new MicrophoneDevice
            {
                name = device,
            });
        }

        if(devices.Contains(device)) 
            return;

        if(devices.Count == 0)
        {
            device = NullDevice.Get;
            return;
        }

        device = devices[0];
    }

    private void StartRecording()
    {
        if (isRecording) return;

        if (device == NullDevice.Get)
        {
            clip = null;
            return;
        }

        clip = Microphone.Start(device.name, true, 20, AudioSettings.outputSampleRate);
        isRecording = true;
    }

    private void StopRecording()
    {
        if (!isRecording) return;

        if(device == NullDevice.Get)
        {
            clip = null;
            return;
        }

        clip = null;
        Microphone.End(device.name);
        isRecording = false;
    }

    private void RestartRecording()
    {
        if (!isRecording) return;

        if(device == NullDevice.Get)
        {
            clip = null;
            return;
        }

        Microphone.End(device.name);
        clip = Microphone.Start(device.name, true, 20, AudioSettings.outputSampleRate);
        isRecording = true;
    }

    private void HandleDeviceListChanged()
    {
        if (Microphone.devices.Count() == devices.Count) return;

        var previousDevice = device;
        LoadMicrophones();

        if(device == NullDevice.Get)
        {
            StopRecording();
        }

        else if(device != previousDevice)
        {
            RestartRecording();
        }

        OnDeviceListChanged?.Invoke();
    }

    private void UpdateLoudnessFromMicrophone()
    {
        if (!isRecording || clip == null) return;

        if (device == NullDevice.Get)
        {
            StopRecording();
            return;
        }

        loudness = GetLoudness(Microphone.GetPosition(device.name), clip);
    }
 
    private float GetLoudness(int position, AudioClip clip)
    {
        if (position <= minSamples) return 0;

        int channels = clip.channels;
        int start = position - sampleWindow * channels;

        if (start < 0) start = 0;

        int totalSamples = position - start;

        if (totalSamples < minSamples) return 0;

        float[] waveData = new float[totalSamples];
        clip.GetData(waveData, start);

        float loudness = 0;
        for(int i = 0; i < totalSamples; i++)
        {
            loudness += Mathf.Abs(waveData[i]);
        }

        return loudness / totalSamples;
    }

    public void ChangeDevice(int deviceIndex)
    {
        if(deviceIndex >= 0 && deviceIndex < devices.Count)
        {
            device = devices[deviceIndex];
            RestartRecording();
        }
    }
}

[Serializable]
public struct MicrophoneDevice
{
    public string name;

    public MicrophoneDevice(string name)
    {
        this.name = name;
    }

    public bool Equals(MicrophoneDevice other)
    {
        return name == other.name;
    }

    public override bool Equals(object obj)
    {
        return obj is MicrophoneDevice other && Equals(other);
    }

    public static bool operator ==(MicrophoneDevice lhs, MicrophoneDevice rhs) => lhs.Equals(rhs);
    public static bool operator !=(MicrophoneDevice lhs, MicrophoneDevice rhs) => !(rhs == lhs);
}

public static class NullDevice
{
    public static MicrophoneDevice Get { get; } = new MicrophoneDevice(string.Empty);
}
