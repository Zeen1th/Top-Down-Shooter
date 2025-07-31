// EnemyAI.cs
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyData enemyData;
    public Transform Player { get; private set; }
    public float MoveSpeed { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private int health;
    private IEnemyState currentState;

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

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = enemyData.moveSpeed;
        health = enemyData.health;
        Rigidbody = GetComponent<Rigidbody>();

        SetState(new ChaseState());
    }

    void FixedUpdate()
    {
        currentState?.UpdateState(this);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            SetState(new DeadState());
        }
    }

    public void DropPowerUp()
    {
        if (Random.value > dropChance) return;

        GameObject drop = Random.value < 0.5f ? fireRatePowerUpPrefab : healPowerUpPrefab;
        if (drop != null)
        {
            Instantiate(drop, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        }
    }

    public void SetState(IEnemyState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
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
