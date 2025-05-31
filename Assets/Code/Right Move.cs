using UnityEngine;

public class RightMove : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {

        AddForceRight();
        
    }

    void AddForceRight()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.right * velocity);
        }
    }
}
