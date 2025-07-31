using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private List<EnemyData> enemyTypes;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float despawnRadius = 20f;
    [Range(20f, 180f)]
    [SerializeField] private float spawnConeAngle = 90f;

    [Header("References")]
    [SerializeField] private Transform playerTransform;

    private float timer;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Vector3 lastPlayerPosition;
    private Vector3 moveDirection;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = FlattenY(playerTransform.position);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 currentPosFlat = FlattenY(playerTransform.position);
            Vector3 delta = currentPosFlat - lastPlayerPosition;
            moveDirection = delta.magnitude > 0.01f ? delta.normalized : moveDirection;
            lastPlayerPosition = currentPosFlat;
        }

        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = spawnedEnemies[i];
            if (enemy == null)
            {
                spawnedEnemies.RemoveAt(i);
                continue;
            }

            float distance = Vector3.Distance(FlattenY(playerTransform.position), FlattenY(enemy.transform.position));
            if (distance > despawnRadius)
            {
                Destroy(enemy);
                spawnedEnemies.RemoveAt(i);
            }
        }

        if (spawnedEnemies.Count < maxEnemies)
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyTypes.Count == 0 || playerTransform == null) return;

        Vector3 coneDirection = moveDirection != Vector3.zero ? moveDirection : FlattenY(playerTransform.forward);
        float halfCone = spawnConeAngle / 2f;
        float angleOffset = Random.Range(-halfCone, halfCone);
        Vector3 spawnDirection = Quaternion.Euler(0, angleOffset, 0) * coneDirection;
        spawnDirection.Normalize();

        Vector3 playerPosFlat = FlattenY(playerTransform.position);
        Vector3 spawnPosition = playerPosFlat + spawnDirection * spawnRadius;
        spawnPosition.y = playerTransform.position.y;

        EnemyData data = enemyTypes[Random.Range(0, enemyTypes.Count)];

        // Load and instantiate the prefab from Addressables
        data.prefab.InstantiateAsync(spawnPosition, Quaternion.identity).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject newEnemy = handle.Result;
                EnemyAI ai = newEnemy.GetComponent<EnemyAI>();
                if (ai != null) ai.enemyData = data;

                spawnedEnemies.Add(newEnemy);
            }
            else
            {
                Debug.LogWarning($"Failed to spawn enemy: {data.enemyName}");
            }
        };
    }

    private Vector3 FlattenY(Vector3 v) => new Vector3(v.x, -2.0f, v.z);

    void OnDrawGizmosSelected()
    {
        if (playerTransform == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, spawnRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, despawnRadius);
    }
}
