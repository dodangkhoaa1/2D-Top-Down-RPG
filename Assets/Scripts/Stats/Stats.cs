using System;
using UnityEngine;

public abstract class Stats : ScriptableObject
{
    public abstract void Save();
    public abstract void Load();
    public abstract void UpgradeCharacter(Action OnSuccess = null, Action OnFail = null);
    public abstract void UpgradeWeapon(Action OnSuccess = null, Action OnFail = null);
    public abstract bool IsCharacterMaxLevel();
    public abstract bool IsWeaponLevelMoreThan5TimeCharacterLevel();
}
