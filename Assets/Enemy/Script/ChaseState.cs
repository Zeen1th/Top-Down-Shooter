// ChaseState.cs
using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        // You could add a chase animation trigger here
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (enemy.Player == null) return;

        Vector3 direction = (enemy.Player.position - enemy.transform.position).normalized;
        direction.y = 0f;

        enemy.Rigidbody.linearVelocity = direction * enemy.MoveSpeed;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemy.Rigidbody.MoveRotation(Quaternion.Slerp(enemy.Rigidbody.rotation, lookRotation, Time.fixedDeltaTime * 5f));
        }
    }
}
