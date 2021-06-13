using UnityEngine;

namespace _NBGames.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public string ItemName;
        public bool IsWeapon;
        public string Description;
        public int SellValue;
    }
}
