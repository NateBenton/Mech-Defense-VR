using System;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    [CreateAssetMenu(fileName = "NewWeaponUpgrade", menuName = "WeaponUpgrade")]
    public class WeaponUpgrade : ScriptableObject
    {
        public WeaponUpgradeFields[] WeaponUpgrades;
    }
}
