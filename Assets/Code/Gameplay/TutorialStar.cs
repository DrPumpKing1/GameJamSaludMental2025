using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TutorialStar : MonoBehaviour
{
    [Header("Spawn")] 
    [SerializeField] private float _maxSpawnHeight = 4.5f;
    private bool _playerTouching;

    [Header("Tutorial")] 
    [SerializeField] private float _timeOnStar;
    public float TimeOnStar => _timeOnStar;
    private float _currentTimeOnStar;
    public float CurrentTimeOnStar => _currentTimeOnStar;
    [SerializeField] private string _gameSceneName = "Juego";

    private void Start()
    {
        SetHeight();
    }

    private void Update()
    {
        if(_playerTouching) _currentTimeOnStar += Time.deltaTime;
        if(_currentTimeOnStar >= _timeOnStar) GoToGame();
    }

    private void GoToGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    private void SetHeight()
    {
        float height = Random.Range(0, _maxSpawnHeight);
        transform.position += Vector3.up * height;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) _playerTouching = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) _playerTouching = false;
    }
}
