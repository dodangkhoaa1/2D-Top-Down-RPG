using System.Collections;
using UnityEngine;

public class Chameleon : MonoBehaviour, IEnemy
{
    private EnemyPathfinding enemyPathfinding;
    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    public void Attack()
    {
        Vector2 directionToPlayer = PlayerController.Instance.transform.position - transform.position;
        enemyPathfinding.MoveTo(directionToPlayer);
    }

}
