using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;
    public float fireRateMultiplier = 0.5f;
    public int healAmount = 25;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        WeaponShooter shooter = other.GetComponent<WeaponShooter>();

        if (type == PowerUpType.FireRateBoost && shooter != null)
        {
            shooter.PermanentlyBoostFireRate(fireRateMultiplier);
        }
        else if (type == PowerUpType.Heal && stats != null)
        {
            stats.Heal(healAmount);
        }

        Destroy(gameObject);
    }
}
