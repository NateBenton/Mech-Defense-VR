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

        public static event Action onShowUpgradeOptions;

        public static void ShowUpgradeOptions()
        {
            onShowUpgradeOptions?.Invoke();
        }

        public static event Action onBackButtonClicked;
        public static void BackButtonClicked()
        {
            onBackButtonClicked?.Invoke();
        }

        public static event Action<string, string, int> onShowWeaponInfo;

        public static void ShowWeaponInfo(string itemName, string description, int price)
        {
            onShowWeaponInfo?.Invoke(itemName, description, price);
        }

        public static event Action onEnableInsufficientFundsText;
        public static void EnableInsufficientFundsText()
        {
            onEnableInsufficientFundsText?.Invoke();
        }

        public static event Action onEnablePurchasedText;

        public static void EnablePurchasedText()
        {
            onEnablePurchasedText?.Invoke();
        }

        public static event Action onEnableShopNextButton;
        public static void EnableShopNextButton()
        {
            onEnableShopNextButton?.Invoke();
        }
        
        public static event Action onDisableShopNextButton;
        public static void DisableShopNextButton()
        {
            onDisableShopNextButton?.Invoke();
        }

        public static event Action onEnableShopPreviousButton;
        public static void EnableShopPreviousButton()
        {
            onEnableShopPreviousButton?.Invoke();
        }
        
        public static event Action onDisableShopPreviousButton;
        public static void DisableShopPreviousButton()
        {
            onDisableShopPreviousButton?.Invoke();
        }
        
        public static event Action onEnableUpgradeNextButton;
        public static void EnableUpgradeNextButton()
        {
            onEnableUpgradeNextButton?.Invoke();
        }
        
        public static event Action onDisableUpgradeNextButton;
        public static void DisableUpgradeNextButton()
        {
            onDisableUpgradeNextButton?.Invoke();
        }

        public static event Action onEnableUpgradePreviousButton;
        public static void EnableUpgradePreviousButton()
        {
            onEnableUpgradePreviousButton?.Invoke();
        }
        
        public static event Action onDisableUpgradePreviousButton;
        public static void DisableUpgradePreviousButton()
        {
            onDisableUpgradePreviousButton?.Invoke();
        }

        public static event Action onDisableUpgradeNavigation;
        public static void DisableUpgradeNavigation()
        {
            onDisableUpgradeNavigation?.Invoke();
        }

        public static event Action onResetNavigationButtons;
        public static void ResetNavigationButtons()
        {
            onResetNavigationButtons?.Invoke();
        }

        public static event Action onMakeUpgradePurchasable;
        public static void MakeUpgradePurchasable()
        {
            onMakeUpgradePurchasable?.Invoke();
        }

        public static event Action onUpgradeWeapon;
        public static void UpgradeWeapon()
        {
            onUpgradeWeapon?.Invoke();
        }

        public static event Action onGripReleased;
        public static void GripReleased()
        {
            onGripReleased?.Invoke();
        }

        public static event Action onWeaponFullyUpgraded;
        public static void WeaponFullyUpgraded()
        {
            onWeaponFullyUpgraded?.Invoke();
        }

        public static event Action onRefreshWeaponDamage;
        public static void RefreshWeaponDamage()
        {
            onRefreshWeaponDamage?.Invoke();
        }

        public static event Action onAllCurrentEnemiesKilled;
        public static void AllCurrentEnemiesKilled()
        {
            onAllCurrentEnemiesKilled?.Invoke();
        }

        public static event Action<GameObject> onEnemyKilled;
        public static void EnemyKilled(GameObject enemy)
        {
            onEnemyKilled?.Invoke(enemy);
        }
    }
}
