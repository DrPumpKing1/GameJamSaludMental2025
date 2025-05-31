using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private int currentEnemyCount = 0;
    [SerializeField] private float nextSpawnTime = 0f;

    void Update()
    {
        if (currentEnemyCount < maxEnemies && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        currentEnemyCount++;
    }

    public void DestroyEnemy()
    {
        currentEnemyCount--;
    }
}
