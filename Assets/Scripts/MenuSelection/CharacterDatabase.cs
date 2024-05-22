using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : ScriptableObject 
{
    public Character[] character;

    public int characterCount
    {
        get
        {
            return character.Length;
        }
    }

    public Character GetCharacter(int index)
    {
        return character[index];
    }
    public void UpdateCharacterUnlockedStatus(int index)
    {
        if (index >= 0 && index < character.Length)
        {
            character[index].Unlock();
        }
    }
}
