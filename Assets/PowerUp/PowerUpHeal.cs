using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private int healAmount = 25;
    [SerializeField] private float hoverSpeed = 2f;
    [SerializeField] private float hoverHeight = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float hover = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = startPos + new Vector3(0, hover, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
