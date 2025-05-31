using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsCheck check;

    [Header("Settings")]
    [SerializeField] private float impulseBase = 12f;
    [SerializeField] private float cooldown = .45f;
    [SerializeField] private float departureTime = .2f;
    private float departureTimer;
    public bool onDeparture { get; private set; }
    public bool OnJump { get; private set; }
    private float timer;
    [SerializeField] private float coyoteTime = .25f;
    [SerializeField] private float fallGravityMultiplier = 3;
    [SerializeField] private float lowGravityMultiplier = 2;
    [SerializeField] private float baseGravityScale;
    [SerializeField] private float buoyancyGravityScale;
    [SerializeField] private float maxSpeed;
    private bool jumpInput;
    public bool isPressed = false;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        if (timer > 0 && !OnJump) timer -= Time.deltaTime;
        if (departureTimer > 0 && !check.IsGrounded) departureTimer -= Time.deltaTime;
        if (departureTimer <= 0 && onDeparture) onDeparture = false;

        if (check.IsGrounded && !onDeparture && OnJump) OnJump = false; //Landing 

        HandleInput();

        body.gravityScale = OnJump && !jumpInput ? baseGravityScale : buoyancyGravityScale;
    }

    private void FixedUpdate()
    {
        if (isPressed) TryJump();

        BetterJump();
        LimitSpeed();
    }

    private void TryJump()
    {
        if (timer > 0) return;

        float timeSinceGrounded = Time.time - check.LastTimeOnGround;
        if (timeSinceGrounded > coyoteTime) return;

        float impulse = impulseBase * MicrophoneInputProcessor.Instance.lastRoundInputState.parameter;

        //body.linearVelocity = new(body.linearVelocity.x, 0);
        body.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        timer = cooldown;
        OnJump = true;
        onDeparture = true;
        departureTimer = departureTime;
    }

    private void BetterJump()
    {
        if (OnJump) return;

        float multiplier = 0;

        if(body.linearVelocity.y < 0)
        {
            multiplier = fallGravityMultiplier;
        }
        else if(!jumpInput)
        {
            multiplier = lowGravityMultiplier;
        }

        if (multiplier == 0) return;

        body.AddForce(Vector2.up * (Physics2D.gravity.y * (multiplier - 1) * Time.fixedDeltaTime), ForceMode2D.Impulse);
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

        if (jumpPressed) TryJump();
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
