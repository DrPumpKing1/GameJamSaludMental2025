using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DeviceDropdowns : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown deviceSelector;

    private void OnEnable()
    {
        MicrophoneInput.Instance.OnDeviceListChanged += SetOptions;
        deviceSelector.onValueChanged.AddListener(OnSelectionChanged);
    }

    private void OnDisable()
    {
        deviceSelector.onValueChanged.RemoveListener(OnSelectionChanged);
    }

    private void Start()
    {
        SetOptions();
    }

    public void SetOptions()
    {
        if (deviceSelector == null) return;

        deviceSelector.ClearOptions();
        List<TMP_Dropdown.OptionData> newOptions = new();
        var devices = MicrophoneInput.Instance.devices;

        if(devices.Count == 0)
        {
            newOptions.Add(new TMP_Dropdown.OptionData("No Device Found"));
        } else
        {
            foreach(var device in devices)
            {
                newOptions.Add(new TMP_Dropdown.OptionData(device.name));
            }
        }

        deviceSelector.AddOptions(newOptions);
        deviceSelector.SetValueWithoutNotify(devices.IndexOf(MicrophoneInput.Instance.device));
    }

    public void OnSelectionChanged(int index)
    {
        MicrophoneInput.Instance?.ChangeDevice(deviceSelector.value);
    }
}
