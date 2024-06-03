using System;
using UnityEngine;

public class ActorStats : Stats
{
    [Header("Base Stats:")]
    public float healthPoint;
    public float damage;
    public float moveSpeed;
    public float knockbackForce;
    public float knockbackTime;
    public float invincibleTime;

    public override bool IsCharacterMaxLevel()
    {
        return false;
    }

    public override bool IsWeaponLevelMoreThan5TimeCharacterLevel()
    {
        return false;
    }

    public override void Load()
    {
    }

    public override void Save()
    {
    }

    public override void UpgradeCharacter(Action OnSuccess = null, Action Onfail = null)
    {
    }
    public override void UpgradeWeapon(Action OnSuccess = null, Action Onfail = null)
    {
    }
}
