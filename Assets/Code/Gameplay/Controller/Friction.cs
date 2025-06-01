using UnityEngine;

public class Friction : MonoBehaviour
{
    private Rigidbody2D body;
    private PhysicsCheck check;
    private Jump jump;

    [Header("Settings")]
    [SerializeField] private float groundFriction = 5f;
    [SerializeField] private float airFriction = 3f;
    public bool IsTurning => InputReader.Instance.Move > 0 && body.linearVelocity.x < 0
        || InputReader.Instance.Move < 0 && body.linearVelocity.x > 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        check = GetComponent<PhysicsCheck>();
        jump = GetComponent<Jump>();
    }

    private void FixedUpdate()
    {
        ManageFriction();
    }

    private void ManageFriction()
    {
        if (!check.IsGrounded)
            ApplyFriction(airFriction);
        else if (IsTurning || InputReader.Instance.Move == 0)
            ApplyFriction(groundFriction);
    }

    private void ApplyFriction(float friction)
    {
        body.AddForce(-Vector2.right * (body.linearVelocity.x * friction * Time.fixedDeltaTime), ForceMode2D.Impulse);
    }
}
