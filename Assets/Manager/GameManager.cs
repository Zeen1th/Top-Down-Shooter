using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    public int Score => score;

    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    private EnemySpawner enemySpawner;
    private PlayerMouseRotation playerMouseRotation;
    private WeaponShooter weaponShooter;

    [SerializeField] private float scoreTickRate = 1f; // how often to add score
    [SerializeField] private int scorePerTick = 1;
    private float scoreTimer;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerMouseRotation = player.GetComponent<PlayerMouseRotation>();
            weaponShooter = player.GetComponent<WeaponShooter>();
        }

        enemySpawner = Object.FindFirstObjectByType<EnemySpawner>();

        if (playerStats != null)
        {
            UIManager.Instance.UpdateHealthUI(playerStats.CurrentHealth, playerStats.GetMaxHealth());
        }

        UIManager.Instance.UpdateScoreUI(score);
    }

    void Update()
    {
        if (playerStats != null && playerStats.CurrentHealth > 0)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= scoreTickRate)
            {
                score += scorePerTick;
                UIManager.Instance.UpdateScoreUI(score);
                scoreTimer = 0f;
            }
        }
        else if (playerStats.CurrentHealth <= 0)
        {
            UIManager.Instance.GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScoreUI(score);
    }

    public void RefreshHealthUI()
    {
        if (playerStats != null)
        {
            UIManager.Instance.UpdateHealthUI(playerStats.CurrentHealth, playerStats.GetMaxHealth());
        }
    }
}
