using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    private Rigidbody2D body;
    private CapsuleCollider2D capsule;
    private Jump jump;
    [SerializeField] private Transform baseLine;
    [SerializeField] private float distanceToDecelerate;
    public bool underBuoyancy { get; private set; }
    private float acceleration;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        jump = GetComponent<Jump>();
    }

    private void FixedUpdate()
    {
        KinematicBuoyancy();
    }

    private Vector3 CenterPosition()
    {
        return transform.position + Vector3.up * capsule.size.y / 2;
    }

    private void KinematicBuoyancy()
    {
        float distance = baseLine.position.y - CenterPosition().y;
        if (distance <= 0)
        {
            underBuoyancy = false;
            return;
        }

        if(underBuoyancy == false)
        {
            acceleration = Mathf.Pow(body.linearVelocity.y, 2) / (2 * distanceToDecelerate);
        }
        underBuoyancy = true;

        if(body.linearVelocity.y >= 0)
        {
            return;
        }
        float gravity = Physics.gravity.y * jump.gravity;
        float forceToApply = (acceleration - gravity) * Time.fixedDeltaTime;

        body.AddForce(Vector3.up * forceToApply, ForceMode2D.Impulse);
    }
}
