using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsCheck check;

    [Header("Settings")]
    [SerializeField] private float impulseBase = 12f;
    [SerializeField] private float cooldown = .45f;
    private float timer;
    [SerializeField] private float coyoteTime = .25f;
    [SerializeField] private float fallGravityMultiplier = 3;
    [SerializeField] private float lowGravityMultiplier = 2;
    [SerializeField] private float maxSpeed;
    private bool jumpInput;
    public float gravity { get; private set; }
    public bool isPressed = false;

    // --- Nuevas variables para el filtro de paso bajo ---
    [Header("Audio Smoothing")]
    [SerializeField] private float lowPassFilterStrength = 0.1f; // Ajusta este valor (0.0 a 1.0), menor valor = más suavizado
    private float smoothedLoudness = 0f; // Almacena el valor de sonoridad suavizado
    [SerializeField] private float positionLerpSpeed = 5f; // Velocidad de interpolación para la posición

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        Vector2 position = transform.position;
        position.y = Mathf.Clamp(position.y, -4.4f, 2.65f);
        transform.position = position;
    }
    private void FixedUpdate()
    {
        //if (timer > 0) timer -= Time.deltaTime;

        HandleInput();
    }

    //private void FixedUpdate()
    //{
    //    //BetterJump();
    //    //LimitSpeed();
    //}

    private void TryJump()
    {
        //if (timer > 0) return;

        //InputRange inputRange = MicrophoneInputProcessor.Instance.lastRoundInputState;
        //float impulse = impulseBase * inputRange.parameter;

        //if(body.linearVelocity.y < 0)
        //{
        //    impulse -= body.linearVelocity.y;
        //}

        //body.AddForce(Vector2.up * MicrophoneInput.Instance.loudness, ForceMode2D.Impulse);

        // --- Aplicar el filtro de paso bajo a la sonoridad ---
        // Inicializa smoothedLoudness la primera vez o si es cero
        if (smoothedLoudness == 0f)
        {
            smoothedLoudness = MicrophoneInput.Instance.loudness;
        }
        // Aplica el filtro de paso bajo para suavizar los cambios
        smoothedLoudness = Mathf.Lerp(smoothedLoudness, MicrophoneInput.Instance.loudness, lowPassFilterStrength);

        print(MicrophoneInput.Instance.loudness); // Sonoridad original
        //print("Smoothed Loudness: " + smoothedLoudness); // Sonoridad suavizada

        // Calcula la posición objetivo usando la sonoridad suavizada
        Vector2 target = new Vector2(body.position.x, smoothedLoudness - 4.5f);
        //print(target);

        // Interpola la posición del cuerpo hacia el objetivo de forma suavizada
        // El uso de Time.deltaTime * positionLerpSpeed asegura que la velocidad de interpolación
        // sea independiente del framerate.
        body.position = Vector2.Lerp(body.position, target, Time.fixedDeltaTime * positionLerpSpeed);
        //timer = cooldown;
    }

    //private void BetterJump()
    //{
    //    if (check.IsGrounded) return;

    //    gravity = 0;
    //    if(body.linearVelocity.y < 0)
    //    {
    //        gravity = fallGravityMultiplier;
    //    }
    //    else if(!jumpInput)
    //    {
    //        gravity = lowGravityMultiplier;
    //    }

    //    if (gravity == 0) return;

    //    body.AddForce(Vector2.up * (Physics2D.gravity.y * gravity * Time.fixedDeltaTime), ForceMode2D.Impulse);
    //}

    //private void LimitSpeed()
    //{
    //    if (Mathf.Abs(body.linearVelocity.y) > maxSpeed)
    //    {
    //        body.linearVelocity = new(body.linearVelocity.x, Mathf.Sign(body.linearVelocity.y) * maxSpeed);
    //    }
    //}

    private void HandleInput()
    {
        //bool previousInput = jumpInput;
        //jumpInput = InputActivation(MicrophoneInputProcessor.Instance.lastRoundInputState);

        //bool jumpPressed = previousInput == false && jumpInput == true;
        //bool jumpReleased = previousInput == true && jumpInput == false;

        /*if (jumpInput)*/
        TryJump();
    }

    static bool InputActivation(InputRange lastInput) => lastInput.level >= 1;

    public void OnPointerDown()
    {
        isPressed = true;
    }

    public void OnPointerUp()
    {
        isPressed = false;
    }
}
