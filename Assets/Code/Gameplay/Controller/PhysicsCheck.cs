using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D capsule;

    private const float ExtraDistance = .01f;
    [SerializeField] private LayerMask ground;

    [Header("Runtime")]
    [SerializeField] private bool isGrounded;
    public bool IsGrounded => isGrounded;
    [SerializeField] private float lastTimeOnGround;
    public float LastTimeOnGround => lastTimeOnGround;

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        //GroundCheck();
        ImmersionGroundCheck();
        if (isGrounded) lastTimeOnGround = Time.time;
    }

    private void GroundCheck()
    {
        var size = capsule.size;
        var position = transform.position + Vector3.up * size.y / 2;

        var footSize = new Vector2(size.x / 2, ExtraDistance);
        var distance = size.y / 2 + ExtraDistance;

        RaycastHit2D hit = Physics2D.BoxCast(position, footSize, 0f, Vector2.down, distance, ground);

        isGrounded = hit.collider != null;
    }

    private void ImmersionGroundCheck()
    {
        var position = transform.position + Vector3.up * capsule.size.y / 2;

        isGrounded = Physics2D.OverlapPoint(position, ground) != null;
    }
}
