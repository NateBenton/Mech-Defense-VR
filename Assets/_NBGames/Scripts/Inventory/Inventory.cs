using System.Collections.Generic;
using UnityEngine;

namespace _NBGames.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> _itemSlots = new List<ItemSlot>();

        public List<ItemSlot> ItemSlots => _itemSlots;
    }
}
