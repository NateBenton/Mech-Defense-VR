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

        public static event Action<int> onRemoveMoney;

        public static void RemoveMoney(int amountToRemove)
        {
            onRemoveMoney?.Invoke(amountToRemove);
        }

        public static event Action<Item, int> onAddItemToInventory;
        public static void AddItemToInventory(Item itemToAdd, int quantity)
        {
            onAddItemToInventory?.Invoke(itemToAdd, quantity);
        }

        public static event Action onShowBuyOptions;
        public static void ShowBuyOptions()
        {
            onShowBuyOptions?.Invoke();
        }

        public static event Action<string, string, int> onShowWeaponInfo;

        public static void ShowWeaponInfo(string itemName, string description, int price)
        {
            onShowWeaponInfo?.Invoke(itemName, description, price);
        }

        public static event Action onShowInsufficientFundsText;
        public static void ShowInsufficientFundsText()
        {
            onShowInsufficientFundsText?.Invoke();
        }

        public static event Action onShowPurchasedText;

        public static void ShowPurchasedText()
        {
            onShowPurchasedText?.Invoke();
        }

        public static event Action onToggleShopNextButton;
        public static void ToggleShopNextButton()
        {
            onToggleShopNextButton?.Invoke();
        }

        public static event Action onToggleShopPreviousButton;
        public static void ToggleShopPreviousButton()
        {
            onToggleShopPreviousButton?.Invoke();
        }
    }
}
