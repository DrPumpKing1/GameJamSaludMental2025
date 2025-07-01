using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class TransitionToGame : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectsToActivate;
    [SerializeField] private GameObject[] ObjectsToDeactivate;
    [SerializeField] private MonoBehaviour[] ComponentsToActivate;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform tutorialPosition;
    [SerializeField] private float transitionDuration;
    [SerializeField] private AnimationCurve transitionCurve;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        SetTutorial();
    }

    public void SetTutorial()
    {
        foreach (var go in ObjectsToActivate)
        {
            go.SetActive(false);
        }

        foreach (var go in ObjectsToDeactivate)
        {
            go.SetActive(true);
        }

        foreach (var component in ComponentsToActivate)
        {
            component.enabled = false;
        } 
        
        _player.position = tutorialPosition.position;
    }

    public void StartGame()
    {
        foreach (var go in ObjectsToActivate)
        {
            go.SetActive(true);
        }

        foreach (var go in ObjectsToDeactivate)
        {
            go.SetActive(false);
        }

        foreach (var component in ComponentsToActivate)
        {
            component.enabled = true;
        }

        StartCoroutine(MovePlayerToPosition());
    }

    private IEnumerator MovePlayerToPosition()
    {
        float elapsedTime = 0;
        Vector3 startPosition = _player.position;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = transitionCurve.Evaluate(Mathf.Clamp01(elapsedTime / transitionDuration));
            _player.position = Vector3.Lerp(startPosition, playerPosition.position, t);
            yield return null;
        }
        
        _player.position = playerPosition.position;
    }
}
