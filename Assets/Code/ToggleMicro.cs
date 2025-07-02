using UnityEngine;
using UnityEngine.UI;

public class ToggleMicro : MonoBehaviour
{
    [SerializeField] private Toggle movementToggle;

    [SerializeField] private MonoBehaviour simpleMovement;

    [SerializeField] private MonoBehaviour[] complexMovements;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float rbN;

    private void Start()
    {
        OnToggleChanged(movementToggle.isOn);
        movementToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool isOn)
    {
        simpleMovement.enabled = !isOn;
        rb.bodyType = isOn ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        /*rb.gravityScale = isOn ? 0.01f : rbN;
        rb.linearDamping = isOn ? 3f : 0f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;*/

        foreach (var movement in complexMovements)
        {
            if (movement != null)
                movement.enabled = isOn;
        }
    }
}
