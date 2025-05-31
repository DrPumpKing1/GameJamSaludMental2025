using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score { get; private set; }
    public bool isRunning { get; private set; } = true;

    [SerializeField] private float scorePerSec;
    [SerializeField] private int unit;
    private float cache;


    private void Update()
    {
        if (!isRunning) return;
            
        cache += scorePerSec * Time.deltaTime;

        if (cache < unit) return;

        cache -= unit;
        AddScore(unit);
    }

    public void AddScore(int score)
    {
        this.score += score;
    }
}
