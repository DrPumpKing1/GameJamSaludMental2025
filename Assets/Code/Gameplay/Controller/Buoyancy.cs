using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private Rigidbody2D body;
    private CapsuleCollider2D capsule;
    private Jump jump;
    [SerializeField] private Transform baseLine;

    [Header("Settings")]
    [SerializeField] private float threshold = 1f;
    [SerializeField] private float maxForce = 15f;
    [SerializeField] private float unitForce = 4f;
    [SerializeField] private float attenuation = .5f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        jump = GetComponent<Jump>();
    }

    private void FixedUpdate()
    {
        ApplyBuoyancy();
    }

    private Vector3 CenterPosition()
    {
        return transform.position + Vector3.up * capsule.size.y / 2;
    }

    private void ApplyBuoyancy()
    {
        float yDiff = baseLine.position.y - CenterPosition().y;
        if (yDiff <= 0)
        {
            if(!jump.OnJump)
            {
                var velocityY = body.linearVelocity.y;
                velocityY = Mathf.Lerp(velocityY, 0, attenuation * Time.fixedDeltaTime);
                body.linearVelocity = new(body.linearVelocity.x, velocityY);
            }
            return;
        }

        float force = Mathf.Abs(yDiff * unitForce);
        if (force < threshold) return;
        force = Mathf.Min(force, maxForce);
        var direction = Vector2.up;

        body.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
}
