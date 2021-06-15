using System;
using _NBGames.Scripts.Inventory;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    [CreateAssetMenu(fileName = "NewWeaponUpgrade", menuName = "WeaponUpgrade")]
    public class WeaponUpgrade : ScriptableObject
    {
        public Item AssociatedItem; 
        public WeaponUpgradeFields[] WeaponUpgrades;
    }
}
