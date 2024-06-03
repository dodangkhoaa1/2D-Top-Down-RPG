using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private float damageAmount;

    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        //damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
        damageAmount = PlayerController.Instance.playerStats.damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
