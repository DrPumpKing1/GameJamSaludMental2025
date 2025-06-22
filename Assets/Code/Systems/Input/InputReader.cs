using System;
using UnityEngine;

public class InputReader : Singleton<InputReader>
{
    private PlayerControls input;
    public float Move => input.Gameplay.Move.ReadValue<float>();

    protected override void Awake()
    {
        base.Awake();
        input = new ();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
