using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MovementPlayer : MonoBehaviour
{

    [SerializeField] private float velocityUp = 5f;
    private Rigidbody2D rb;
    private bool useMicrophone = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        int micValue = PlayerPrefs.GetInt("UseMicrophone", 0);
        useMicrophone = false;

        useMicrophone = micValue == 1;

        if (useMicrophone)
        {
            Debug.Log("Modo: Micrófono");
        }
        else
        {
            Debug.Log("Modo: Tap");
        }
    }

    void Update()
    {
        if (!useMicrophone)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Tap/Clic/Barra detectado");
                AddForceUp();
            }
        }
        else
        {
            if (MicrophoneIsLoudEnough())
            {
                AddForceUp();
            }
        }

        Vector2 position = transform.position;
        position.y = Mathf.Clamp(position.y, -4.4f, 2.65f);
        transform.position = position;
    }

    void AddForceUp()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * velocityUp, ForceMode2D.Impulse);
        }
    }

    private bool MicrophoneIsLoudEnough()
    {
        return false;
    }
}