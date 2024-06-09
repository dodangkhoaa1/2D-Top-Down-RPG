using System;
using UnityEngine;

public class ActorStats : Stats
{
    [Header("Base Stats:")]
    public float healthPoint = 3f; //max health
    public float damage = 1f;
    public float moveSpeed = 4f;
    public float knockbackForce = 15f;
    public float knockbackTime = 0.2f;
    public float invincibleTime = 1f;

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
