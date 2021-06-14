using System;
using _NBGames.Scripts.Inventory;

namespace _NBGames.Scripts.Shop
{
    [Serializable]
    public class ItemForPurchase
    {
        public Item ItemForSale;
        public bool HasBeenPurchased;
    }
}
