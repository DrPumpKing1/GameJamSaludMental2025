using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Turning : MonoBehaviour
{
    public float turnAngle = 10f;     // Máximo ángulo de rotación
    public float turnSpeed = 5f;      // Velocidad de interpolación

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float targetZ = 0f;

        if (rb.linearVelocity.x < -0.01f)
        {
            targetZ = -turnAngle;
        }
        else if (rb.linearVelocity.x > 0.01f)
        {
            targetZ = turnAngle;
        }

        // Obtenemos el ángulo Z actual del objeto (en grados)
        float currentZ = transform.rotation.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f; // Convertimos a rango -180 a 180

        // Interpolamos suavemente entre el ángulo actual y el deseado
        float newZ = Mathf.LerpAngle(currentZ, targetZ, Time.deltaTime * turnSpeed);

        // Aplicamos la nueva rotación
        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }
}