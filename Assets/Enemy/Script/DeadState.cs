// DeadState.cs
using UnityEngine;

public class DeadState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        enemy.DropPowerUp();
        Object.Destroy(enemy.gameObject);
    }

    public void UpdateState(EnemyAI enemy)
    {
        // Dead — do nothing
    }
}
