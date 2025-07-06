using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 1;

    private Vector3 flatDirection;

    private void Start()
    {
        flatDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += flatDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
