using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float moveSpeed;
    public int health;
    public int damage;

    [Header("Addressable Prefab")]
    public AssetReferenceGameObject prefab;
}
