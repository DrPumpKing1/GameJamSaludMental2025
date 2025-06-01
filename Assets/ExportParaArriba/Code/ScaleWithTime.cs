using UnityEngine;

public class ScaleWithTime : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float interval;
    private Vector3 target;
    private Vector3 start;
    private Vector3 initialScale;
    private float timer;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = interval;
            Flip();
        }

        Scale();
    }

    private void Scale()
    {
        float parameter = Mathf.Clamp01(timer / interval);
        transform.localScale = Vector3.Lerp(start, target, parameter);
    }
   
    private void Flip()
    {
        if(start == initialScale)
        {
            start = initialScale * (1 + scaleMultiplier);
            target = initialScale;
        } else
        {
            start = initialScale;
            target = initialScale * (1 + scaleMultiplier);
        }
    }
}
