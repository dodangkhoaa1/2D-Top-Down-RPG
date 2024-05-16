using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class MapDatabase : ScriptableObject
{
    public Map[] maps;

    public int mapCount
    {
        get
        {
            return maps.Length;
        }
    }

    public Map GetMap(int index)
    {
        return maps[index];
    }

    public void UpdateMapUnlockedStatus(int index)
    {
        if (index >= 0 && index < maps.Length)
        {
            maps[index].Unlock();
        }
    }
}
