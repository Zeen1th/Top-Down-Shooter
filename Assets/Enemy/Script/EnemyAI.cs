using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;

    private Transform player;
    private float moveSpeed;
    private int health;
    private Rigidbody rb;

    [Header("Power Up Drops")]
    [SerializeField] private GameObject fireRatePowerUpPrefab;
    [SerializeField] private GameObject healPowerUpPrefab;
    [SerializeField, Range(0f, 1f)] private float dropChance = 0.25f;

    void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyData not assigned!");
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = enemyData.moveSpeed;
        health = enemyData.health;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        rb.linearVelocity = direction * moveSpeed;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, Time.fixedDeltaTime * 5f));
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DropPowerUp();
            Destroy(gameObject);
        }
    }

    private void DropPowerUp()
    {
        if (Random.value > dropChance) return;

        GameObject drop = Random.value < 0.5f ? fireRatePowerUpPrefab : healPowerUpPrefab;
        if (drop != null)
        {
            Instantiate(drop, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats != null && enemyData != null)
            {
                stats.TakeDamage(enemyData.damage);
            }
        }
    }
}
