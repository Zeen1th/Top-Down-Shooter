using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        CurrentHealth = maxHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            if (playerMovement != null) playerMovement.enabled = false;
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        UIManager.Instance.UpdateHealthUI(CurrentHealth, maxHealth);
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        UIManager.Instance.UpdateHealthUI(CurrentHealth, maxHealth);
    }


    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(int newMax)
    {
        maxHealth = newMax;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
    }
}
