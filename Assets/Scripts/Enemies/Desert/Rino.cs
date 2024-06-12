using System.Collections;
using UnityEngine;

public class Rino : MonoBehaviour, IEnemy
{
    private EnemyPathfinding enemyPathfinding;
    private Vector2 toPlayerDirection;
    private Vector3 playerPosition;
    private Rigidbody2D rb;

    public float retreatDistance = 3f; // Khoảng cách lùi lại trước khi tấn công
    public float retreatSpeed = 1f; // Tốc độ lùi lại
    public float retreatDuration = 1.5f; // Thời gian lùi lại

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdatePlayerPosition();
    }

    private void Update()
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        playerPosition = PlayerController.Instance.transform.position;
        toPlayerDirection = (playerPosition - transform.position).normalized;
    }

    public void Attack()
    {
        RetreatAndAttack();

        // Attacking
        enemyPathfinding.MoveTo(playerPosition - transform.position);
    }

    private void RetreatAndAttack()
    {
        Vector3 retreatPosition = transform.position - (Vector3)toPlayerDirection * retreatDistance;

        // Retreating
        float elapsedTime = 0f;
        while (elapsedTime < retreatDuration)
        {
            bool check = elapsedTime < retreatDuration;
            Debug.Log(check.ToString());
            rb.MovePosition(Vector2.Lerp(transform.position, retreatPosition, elapsedTime / retreatDuration));
            elapsedTime += Time.deltaTime;
        }

        // Ensuring the enemy has fully retreated
        rb.MovePosition(retreatPosition);

    }
}
