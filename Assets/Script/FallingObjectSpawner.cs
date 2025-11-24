using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] spawnPrefabs; 
    [SerializeField] private float spawnInterval = 2f;  
    [SerializeField] private float minX = -8f;          
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float spawnY = 6f;         
    [SerializeField] private float fallSpeed = 3f;      

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    private void SpawnObject()
    {
        if (spawnPrefabs.Length == 0) return;

        
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

     
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0f);

      
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

      
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = obj.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0f; 
        rb.linearVelocity = Vector2.down * fallSpeed;
    }
}
