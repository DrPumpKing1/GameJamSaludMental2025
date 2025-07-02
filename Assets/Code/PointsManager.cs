using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    public TMP_Text pointsText;
    private int points;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private CurvePoint curveReference;

    private float nearCurveTimer = 0f;
    [SerializeField] private float checkInterval = 0.1f;
    private float checkCooldown = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        points = 0;
        UpdatePointsText();
    }

    private void Update()
    {
        checkCooldown -= Time.deltaTime;
        if (checkCooldown <= 0f)
        {
            checkCooldown = checkInterval;

            if (curveReference != null && playerTransform != null)
            {
                float playerY = playerTransform.position.y;
                float playerX = playerTransform.position.x;

                float curveY = curveReference.GetCurveYAtCurrentTimeAndX(playerX);
                float distanceY = Mathf.Abs(playerY - curveY);

                Debug.Log($"Jugador Y: {playerY} | Curva Y: {curveY} | Distancia: {distanceY}");

                if (distanceY < 1f)
                {
                    nearCurveTimer += checkInterval;
                    Debug.Log($"Cerca de la curva. Tiempo acumulado: {nearCurveTimer}");

                    if (nearCurveTimer >= 1f)
                    {
                        int seconds = Mathf.FloorToInt(nearCurveTimer);
                        int gainedPoints = seconds;

                        Debug.Log($"Sumando {gainedPoints} puntos por cercanía");
                        AddPoint(gainedPoints);
                        nearCurveTimer -= seconds;
                    }
                }
                else
                {
                    nearCurveTimer = 0f;
                }
            }
        }
    }

    public void AddPoint(int amount)
    {
        points += amount;
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    }
}

