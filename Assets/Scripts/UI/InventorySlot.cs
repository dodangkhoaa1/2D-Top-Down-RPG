using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private PlayerStats playerStats; //support get weapon stats

    public PlayerStats PlayerStats() => playerStats;

    public WeaponInfo GetWeaponInfo() => weaponInfo; //support get weapon stats

}
