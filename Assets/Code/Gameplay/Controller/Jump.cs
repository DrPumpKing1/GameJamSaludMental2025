using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsCheck check;

    [Header("Settings")]
    [SerializeField] private float impulse = 12f;
    [SerializeField] private float cooldown = .45f;
    private float timer;
    [SerializeField] private float coyoteTime = .25f;
    [SerializeField] private float fallGravityMultiplier = 3;
    [SerializeField] private float lowGravityMultiplier = 2;
    private bool jumpInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        jumpInput = MicrophoneInputProcessor.Instance.IsHigh;

        if (timer > 0) timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (jumpInput) TryJump();

        BetterJump();
    }

    private void TryJump()
    {
        if (timer > 0) return;

        float timeSinceGrounded = Time.time - check.LastTimeOnGround;
        if (timeSinceGrounded > coyoteTime) return;

        body.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        timer = cooldown;
    }

    private void BetterJump()
    {
        if (check.IsGrounded) return;

        if(body.linearVelocity.y < 0)
        {
            body.AddForce(Vector2.up * (Physics2D.gravity.y * (fallGravityMultiplier - 1) * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
        else if(body.linearVelocity.y > 0 && !jumpInput)
        {
            body.AddForce(Vector2.up * (Physics2D.gravity.y * (lowGravityMultiplier - 1) * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }

}
