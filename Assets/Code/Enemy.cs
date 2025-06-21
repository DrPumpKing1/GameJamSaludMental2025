using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocityLeft = 5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AddVelocityLeft(); 
    }

    private void AddVelocityLeft()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(-velocityLeft, rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PointsBlock")){

            if (collision.gameObject.CompareTag("PointsBlock"))
            {
                PointsManager.Instance?.AddPoint(1);
            }
            Destroy(gameObject);
        }
    }
}
