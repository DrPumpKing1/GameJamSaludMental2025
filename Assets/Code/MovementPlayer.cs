using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float velocityRight = 5f;
    [SerializeField] private float velocityUp = 5f;
    [SerializeField] private Button upButton;
    private bool isPressed = false;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AddVelocityRight();

        if (isPressed)
        {
            AddForceUp();
        }
    }

    void AddVelocityRight()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(velocityRight, rb.linearVelocity.y);
        }
    }
    void AddForceUp()
    {
        Debug.Log("a�aaa");
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * velocityUp, ForceMode2D.Impulse);
        }
    }

    public void OnPointerDown()
    {
        isPressed = true;
    }

    public void OnPointerUp( )
    {
        isPressed = false;
    }

}
