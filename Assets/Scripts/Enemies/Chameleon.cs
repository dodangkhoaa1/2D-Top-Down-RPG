using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : MonoBehaviour, IEnemy
{
    Rigidbody2D rigidbody2d;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {
        rigidbody2d.MovePosition(PlayerController.Instance.transform.position * Time.deltaTime);
        Debug.Log("Attack");
    }
}

