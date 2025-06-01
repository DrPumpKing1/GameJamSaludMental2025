using UnityEngine;

public class VisibilityChecker : MonoBehaviour
{
    public static VisibilityChecker Instance { get; private set; }
    private Camera mainCamera;
    public float width { get; private set; }
    public float left { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mainCamera = Camera.main;
        SetupViewData();
    }

    private void Update()
    {
        UpdateLeft();
    }

    private void SetupViewData()
    {
        UpdateLeft();
        width = GetScreenPositionWorldX(new(Screen.width, 0)) - left;
    }

    private void UpdateLeft()
    {
        left = GetScreenPositionWorldX(new(0, 0));
    }

    private float GetScreenPositionWorldX(Vector2 screenPosition)
    {
        return mainCamera.ScreenToWorldPoint(screenPosition).x;
    }

    public bool PassedViewport(float x, float width)
    {
        return !((x >= left && x <= left + width) || (x + width >= left && x + width <= left + width)) && x < left;
    }
}
