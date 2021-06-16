using System;
using _NBGames.Scripts.Inventory;

namespace _NBGames.Scripts.Shop
{
    [Serializable]
    public class WeaponUpgrade
    {
        public Item AssociatedItem; 
        public WeaponUpgradeFields[] WeaponUpgrades;
    }
}
