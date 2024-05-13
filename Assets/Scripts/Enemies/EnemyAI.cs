using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirectionFloat = 2f; //time to change move's direction
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state; //state of enemy
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming; //set state
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition); //move to random position Vector2(random(-1,1), random(-1;1))

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
            //if distance from enemy to player less than attacking's range, then enemy switch to attack
        {
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDirectionFloat)
            //change direction when time roaming more than time to change direction
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
            //if distance from enemy to player more than attacking range, then roaming
        {
            state = State.Roaming;
        }

        if(attackRange != 0 && canAttack)
            //when have ability to attack
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if(stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine()); //delay for the next attack
        }
    }

    /// <summary>
    /// wait attackCooldown to continue attacking
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    /// <summary>
    /// Random a vector to roaming
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; //random Vector to move of enemy
    }
}
