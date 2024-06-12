using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; //moving speed of enemy

    private Rigidbody2D rb;
    private Vector2 moveDir; //direction to move
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) return; //stop moving of enemy after get knock
        if(rb.bodyType.ToString() != "Static")
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.deltaTime));

        if (moveDir.x < 0)
        {
            FaceToLeft();
        }
        else if (moveDir.x > 0)
        {
            FaceToRight();
        }
    }

    public void MoveTo(Vector2 targetDirection)
    {
        moveDir = targetDirection;
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

    private void FaceToLeft()
    {
        spriteRenderer.flipX = true;
    }

    private void FaceToRight()
    {
        spriteRenderer.flipX = false;
    }
}
