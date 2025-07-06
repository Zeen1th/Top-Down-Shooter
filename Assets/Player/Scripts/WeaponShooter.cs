using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject projectilePrefab;
    [Tooltip("The lower the value, the faster it shoots")]
    [SerializeField] private float fireRate = 0.3f;

    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            FireProjectile();
            fireCooldown = fireRate;
        }
    }

    void FireProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, shootOrigin.position.y);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 direction = (targetPoint - shootOrigin.position).normalized;

            Instantiate(projectilePrefab, shootOrigin.position, Quaternion.LookRotation(direction));
        }
    }

    public void PermanentlyBoostFireRate(float multiplier = 0.5f)
    {
        fireRate *= multiplier;
        Debug.Log($"Fire rate permanently boosted to {fireRate}");
    }
}
