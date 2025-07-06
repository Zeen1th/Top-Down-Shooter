using UnityEngine;

public class PowerUpFireRate : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 0.5f;
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
        WeaponShooter shooter = other.GetComponent<WeaponShooter>();
        if (shooter != null)
        {
            shooter.PermanentlyBoostFireRate(boostMultiplier);
            Destroy(gameObject);
        }
    }
}
