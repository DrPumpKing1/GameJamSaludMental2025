using UnityEngine;
using UnityEngine.UI;

public class LoudnessSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private RectTransform handle;
    [SerializeField] private RectTransform baseUI;

    [Header("Handle Effect Settings")]
    [SerializeField] private float endScaleMultiplier;
    private Vector3 initialLocalScale;
    private Vector3 endLocalScale;
    [SerializeField] private float maxAngleSpeed;
    private float angleSpeed;
    private float angle;
    [SerializeField] private AnimationCurve transition;
    private float parameter;
    float previousLastLoudness;
    float referenceValue;
    [SerializeField] private float transitionDuration;
    float parameterTransitionTime;
    float transitionTimer;

    [Header("Base Effect Settings")]
    [SerializeField] private float maxTiltAngle;
    private float previousTiltAngle;
    private float tiltAngle;
    [SerializeField] private float tiltFrequency;
    private float timeBetweenTilt;
    private float timer;
    private float targetAngle;

    private void Start()
    {
        handle = slider.handleRect;
        initialLocalScale = handle.localScale;
        endLocalScale = initialLocalScale * endScaleMultiplier;
    }

    private void Update()
    {
        Parameter();

        Slider();
        Handle();
        Base();
    }

    private void Parameter()
    {
        float lastLoudness = MicrophoneInputProcessor.Instance.lastRoundInputState.upper;

        if(previousLastLoudness != lastLoudness)
        {
            parameterTransitionTime = transitionDuration * Mathf.Abs(previousLastLoudness - lastLoudness);
            referenceValue = slider.value;
            transitionTimer = 0;
        }

        transitionTimer += Time.deltaTime;

        slider.value = Mathf.Clamp01(Mathf.Lerp(referenceValue, lastLoudness, Mathf.Clamp01(transitionTimer / parameterTransitionTime)));
        parameter = Mathf.Clamp01(transition.Evaluate(slider.value));

        previousLastLoudness = lastLoudness;
    }

    private void Slider()
    {
        slider.value = Mathf.Clamp01(MicrophoneInput.Instance.loudness);
    }

    private void Handle()
    {
        HandleScale();
        HandleRotation();
    }

    private void HandleScale()
    {
        handle.localScale = Vector3.Lerp(initialLocalScale, endLocalScale, parameter);
    }

    private void HandleRotation()
    {
        angleSpeed = Mathf.Lerp(0, maxAngleSpeed, parameter);

        if (angleSpeed <= 0) return;

        angle += angleSpeed * Time.deltaTime;
        handle.rotation = Quaternion.Euler(0, 0, angle % 360);
    }

    private void Base()
    {
        targetAngle = Mathf.Lerp(0, maxTiltAngle * (targetAngle < 0 ? -1 : 1), parameter);
        float frequency = Mathf.Lerp(1, tiltFrequency, parameter);
        timeBetweenTilt = 1 / frequency;

        if (Mathf.Abs(targetAngle) == 0)
        {
            tiltAngle = 0;
            baseUI.rotation = Quaternion.Euler(0, 0, tiltAngle);
            return;
        }

        bool turn = false;
        if (timer < timeBetweenTilt)
        {
            timer += Time.deltaTime;
        }
        else
        {
            turn = true;
        }

        float parameterTilt = Mathf.Clamp01(timer / timeBetweenTilt);
        tiltAngle = Mathf.Lerp(previousTiltAngle, targetAngle, parameterTilt);

        baseUI.rotation = Quaternion.Euler(0, 0, tiltAngle);

        if(turn)
        {
            targetAngle *= -1;
            previousTiltAngle = tiltAngle;
            timer = 0;
        }
    }
}
