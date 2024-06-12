using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        //get direction from damageSource to Knockback and multiple to knockBackThrust
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        //yield return new WaitForSeconds(knockBackTime);
        yield return new WaitForSeconds(PlayerController.Instance.playerStats.knockbackTime);
        if (!rb.bodyType.ToString().Equals("Static")) //the "Plant" enemy has rigidbody type static
        {
            rb.velocity = Vector2.zero;
        }
        GettingKnockedBack = false;
    }
}
