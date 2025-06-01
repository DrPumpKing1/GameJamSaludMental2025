using UnityEngine;

public class EternalyMoveRight : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
