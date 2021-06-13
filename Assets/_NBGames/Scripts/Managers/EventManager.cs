using System;
using _NBGames.Scripts.Inventory;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public static event Action<int> onAddMoney;
        public static void AddMoney(int amountToAdd)
        {
            onAddMoney?.Invoke(amountToAdd);
        }

        public static event Action onShowShop;
        public static void ShowShop()
        {
            onShowShop?.Invoke();
        }

        public static event Action onCloseShop;
        public static void CloseShop()
        {
            onCloseShop?.Invoke();
        }

        public static event Action<Item, int> onAddItemToInventory;
        public static void AddItemToInventory(Item itemToAdd, int quantity)
        {
            onAddItemToInventory?.Invoke(itemToAdd, quantity);
        }
    }
}
