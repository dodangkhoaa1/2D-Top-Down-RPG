using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provide key set int PlayPref
/// </summary>
public static class DatabaseKey
{
    #region Key
    /// <summary>
    /// Key to get selected character option
    /// </summary>
    public static string CharacterSelectedOptionKey
    {
        get
        {
            return "CharacterOption";
        }
    }

    /// <summary>
    /// Key to get selected map option
    /// </summary>
    public static string MapSelectedOptionKey
    {
        get
        {
            return "MapOption";
        }
    }

    /// <summary>
    /// Key to get gold in database store in PlayerPref
    /// </summary>
    public static string PlayerGoldKey
    {
        get
        {
            return "PlayerGold";
        }
    }
    #endregion

    #region Dialog Content
    public static string UnlockCharacter
    {
        get
        {
            return "Please unlock character";
        }
    }

    
    #endregion
}
