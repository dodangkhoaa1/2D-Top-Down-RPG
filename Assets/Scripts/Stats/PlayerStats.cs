using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/NewCharacterStats", order = 1)]
public class PlayerStats : ActorStats
{
    [Header("Level Up Base")]
    public int level;
    public int maxLevel;
    public float xp;
    public float levelUpXpRequired;

    [Header("Level Up:")]
    public float levelUpXpRequiredUp;
    public float hpUp;

    public override bool IsMaxLevel()
    {
        return level > maxLevel;
    }

    public override void Load()
    {
        if(!string.IsNullOrEmpty(Prefs.characterStats))
        JsonUtility.FromJsonOverwrite(Prefs.characterStats, this);
    }

    public override void Save()
    {
        Prefs.characterStats = JsonUtility.ToJson(this);
    }
}
