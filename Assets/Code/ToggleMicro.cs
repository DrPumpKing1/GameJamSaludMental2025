using UnityEngine;
using UnityEngine.UI;

public class ToggleMicro : MonoBehaviour
{
    [SerializeField] private Toggle movementToggle;

    [SerializeField] private MonoBehaviour simpleMovement;

    [SerializeField] private MonoBehaviour[] complexMovements;

    private void Start()
    {
        OnToggleChanged(movementToggle.isOn);
        movementToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool isOn)
    {
        simpleMovement.enabled = !isOn;

        foreach (var movement in complexMovements)
        {
            if (movement != null)
                movement.enabled = isOn;
        }
    }
}
