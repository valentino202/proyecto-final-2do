using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private EnemyShip enemyPrefab;
    [SerializeField] private EnemyDataSO[] enemyTypes;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform[] spawnPoints;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0 || enemyTypes.Length == 0)
            return;

        
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

      
        EnemyShip newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

       
        EnemyDataSO data = enemyTypes[Random.Range(0, enemyTypes.Length)];
        newEnemy.SetData(data);
    }
}
