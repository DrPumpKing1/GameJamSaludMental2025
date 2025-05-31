using UnityEngine;

public class Movement  : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsCheck check;

    [Header("Settings")]
    [SerializeField] private float airMultiplier = .5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 15f;
    public bool isMovementBlocked { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
    }

    private void FixedUpdate()
    {
        Move();
        LimitSpeed();
    }

    private void Move()
    {
        if (isMovementBlocked) return;

        float moveInput = InputReader.Instance.Move;

        if (Mathf.Abs(moveInput) < .1f) return;

        Vector2 moveDirection = Vector2.right * (moveInput * acceleration);
        float multiplier = check.IsGrounded ? 1f : airMultiplier;
        body.AddForce(moveDirection * multiplier);
    }

    private void LimitSpeed()
    {
        if(Mathf.Abs(body.linearVelocity.x) > maxSpeed)
        {
            body.linearVelocity = new(Mathf.Sign(body.linearVelocity.x) * maxSpeed, body.linearVelocity.y);
        }
    }

    public void Block() => isMovementBlocked = true;
    public void Unblock() => isMovementBlocked = false;
}
