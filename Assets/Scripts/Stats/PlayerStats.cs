using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/NewCharacterStats", order = 1)]
public class PlayerStats : ActorStats
{
    [Header("Character Level Up Base:")]
    public int characterLevel;
    public int characterMaxLevel;
    public float characterXp;
    public float characterLevelUpXpRequired;

    [Header("Character Level Up:")]
    public float characterLevelUpXpRequiredUp;
    public float characterHpUp;

    [Header("Weapon Base:")]
    public float weaponCooldown;
    public float weaponDamage;
    public float weaponRange;

    [Header("Weapon Upgrade:")]
    public int weaponLevel;
    public int weaponMaxLevel;
    public int weaponCooldownUp;
    public int weaponRangeUp;
    public int priceToUpgrade;
    public int nextPriceToUpgrade;

    [Header("Limit:")]
    public float minWeaponCooldown = 0.1f;

    public float WeaponCooldownUpInfo { get => weaponCooldownUp * Helper.GetUpgradeFormular(weaponLevel + 1); }
    public float WeaponDamageUpInfo { get => weaponDamage * Helper.GetUpgradeFormular(weaponLevel + 1); }
    public float WeaponRangeUpInfo { get => weaponDamage * Helper.GetUpgradeFormular(weaponLevel + 1); }
    public override bool IsCharacterMaxLevel()
    {
        return characterLevel > characterMaxLevel;
    }

    public override bool IsWeaponLevelMoreThan5TimeCharacterLevel()
    {
        //Giả thuyết level của vũ khí chỉ được hơn 5 lần level của nhân vật 
        return weaponLevel > 5 * characterLevel;
    }
    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.characterStats))
            JsonUtility.FromJsonOverwrite(Prefs.characterStats, this);
    }

    public override void Save()
    {
        Prefs.characterStats = JsonUtility.ToJson(this);
    }

    public override void UpgradeCharacter(Action OnSuccess = null, Action Onfail = null)
    {
        while (characterXp >= characterLevelUpXpRequired
            && !IsCharacterMaxLevel())
        {
            characterLevel++;
            characterXp -= characterLevelUpXpRequired;

            healthPoint += characterHpUp * Helper.GetUpgradeFormular(characterLevel);
            characterLevelUpXpRequired += characterLevelUpXpRequiredUp * Helper.GetUpgradeFormular(characterLevel);

            Save();
            OnSuccess?.Invoke();
        }
        if (characterXp < characterLevelUpXpRequired
            || IsCharacterMaxLevel())
        {
            Onfail?.Invoke();
        }
    }

    public override void UpgradeWeapon(Action OnSuccess = null, Action Onfail = null)
    {
        if (Prefs.IsEnoughCoins(priceToUpgrade)
            && !IsWeaponLevelMoreThan5TimeCharacterLevel())
        {
            Prefs.coins -= priceToUpgrade;

            weaponLevel++;
            weaponCooldown -= weaponCooldownUp * Helper.GetUpgradeFormular(weaponLevel);
            weaponCooldown = Mathf.Clamp(weaponCooldown, minWeaponCooldown, weaponCooldown);

            weaponDamage += weaponDamage * Helper.GetUpgradeFormular(weaponLevel);

            weaponRange += weaponRange * Helper.GetUpgradeFormular(weaponLevel);

            priceToUpgrade += nextPriceToUpgrade * weaponLevel;

            Save();
            OnSuccess?.Invoke();

            return;
        }

        Onfail?.Invoke();
    }
}
