using UnityEngine;

public class CurvePoint : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        float t = 0;
        float v = 0;
        float radio = Mathf.PI * 2f;
        float length = curve.keys[curve.length - 1].time;
        float time = Time.time % length;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector2 point = lineRenderer.GetPosition(i);
            point.x = Mathf.Lerp(-10, 10, t);
            point.y = curve.Evaluate((time + v / radio) % length);

            lineRenderer.SetPosition(i, point);
            t += 1f / lineRenderer.positionCount;
            v += radio / lineRenderer.positionCount;
        }
    }
    public float GetCurveYAtCurrentTimeAndX(float playerX)
    {
        float radio = Mathf.PI * 2f;
        float length = curve.keys[curve.length - 1].time;
        float time = Time.time % length;

        float t = Mathf.InverseLerp(-10f, 10f, playerX);
        float v = radio * t;

        return curve.Evaluate((time + v / radio) % length);
    }
}
