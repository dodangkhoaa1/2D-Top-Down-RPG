using UnityEngine;

public static class Prefs
{
    public static int coins
    {
        set => PlayerPrefs.SetInt(PrefConsts.COIN_KEY, value);
        get => PlayerPrefs.GetInt(PrefConsts.COIN_KEY, 0);
    }  

    public static int characterSelectedOption
    {
        set => PlayerPrefs.SetInt(PrefConsts.CHARACTER_SELECTED_OPTION_KEY, value);
        get => PlayerPrefs.GetInt(PrefConsts.CHARACTER_SELECTED_OPTION_KEY, 0);
    }

    public static int mapSeletedOption
    {
        set => PlayerPrefs.SetInt(PrefConsts.MAP_SELECTED_OPTION_KEY, value);
        get => PlayerPrefs.GetInt(PrefConsts.MAP_SELECTED_OPTION_KEY, 0);
    }

    public static string characterStats
    {
        set => PlayerPrefs.SetString(PrefConsts.CHARACTER_STATS, value);
        get => PlayerPrefs.GetString(PrefConsts.CHARACTER_STATS, "");
    }

    public static bool IsEnoughCoins(int coinToCheck) => coins >= coinToCheck;
}
