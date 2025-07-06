using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float moveSpeed;
    public int health;
    public int damage;
    public GameObject prefab;
}
