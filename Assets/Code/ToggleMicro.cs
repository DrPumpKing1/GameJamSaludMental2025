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
        bool useMic = PlayerPrefs.GetInt("UseMicrophone", 0) == 1;
        if (movementToggle != null)
        {
            movementToggle.isOn = useMic;
            movementToggle.onValueChanged.AddListener(OnToggleChanged);
        }
        OnToggleChanged(useMic);
    }

    public void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("UseMicrophone", isOn ? 1 : 0);
        PlayerPrefs.Save();
        if (simpleMovement != null) simpleMovement.enabled = !isOn;
        if (rb != null)
        {
            rb.gravityScale = isOn ? 0.01f : rbN;
            rb.linearDamping = isOn ? 3f : 0f;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        foreach (var movement in complexMovements)
        {
            if (movement != null)
                movement.enabled = isOn;
        }
    }
}
