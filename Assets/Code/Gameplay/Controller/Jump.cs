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


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;

        HandleInput();
    }

    private void FixedUpdate()
    {
        BetterJump();
        LimitSpeed();
    }

    private void TryJump()
    {
        if (timer > 0) return;

        float impulse = impulseBase * MicrophoneInputProcessor.Instance.lastRoundInputState.parameter;

        if(body.linearVelocity.y < 0)
        {
            impulse -= body.linearVelocity.y;
        }

        body.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        timer = cooldown;
    }

    private void BetterJump()
    {
        if (check.IsGrounded) return;

        gravity = 0;
        if(body.linearVelocity.y < 0)
        {
            gravity = fallGravityMultiplier;
        }
        else if(!jumpInput)
        {
            gravity = lowGravityMultiplier;
        }

        if (gravity == 0) return;

        body.AddForce(Vector2.up * (Physics2D.gravity.y * gravity * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }

    private void LimitSpeed()
    {
        if (Mathf.Abs(body.linearVelocity.y) > maxSpeed)
        {
            body.linearVelocity = new(body.linearVelocity.x, Mathf.Sign(body.linearVelocity.y) * maxSpeed);
        }
    }


    private void HandleInput()
    {
        bool previousInput = jumpInput;
        jumpInput = InputActivation(MicrophoneInputProcessor.Instance.lastRoundInputState);

        bool jumpPressed = previousInput == false && jumpInput == true;
        bool jumpReleased = previousInput == true && jumpInput == false;

        if (jumpInput) TryJump();
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
