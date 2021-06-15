using System;
using System.Collections.Generic;
using _NBGames.Scripts.Shop;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class BuyManager : MonoBehaviour
    {
        [SerializeField] private List<ItemForPurchase> _weaponsForSale = new List<ItemForPurchase>();
        [SerializeField] private List<GameObject> _weaponInteractables = new List<GameObject>();
        [SerializeField] private GameObject _weaponInteractableHolder;

        private int _shownWeaponIndex;
        private GameObject _shownWeapon;

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += ShowWeaponsForPurchase;
        }

        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= ShowWeaponsForPurchase;
        }

        private void Awake()
        {
            if (_weaponsForSale.Count != _weaponInteractables.Count)
            {
                Debug.LogError("WeaponsForSale and WeaponInteractables size mismatch!");
            }
        }

        private void ShowWeaponsForPurchase()
        {
            _weaponInteractableHolder.SetActive(true);
            ToggleButtonsAtSetup();
            UpdateShownWeapon();
        }

        private void ToggleButtonsAtSetup()
        {
            EventManager.ToggleShopPreviousButton();
            if (OnlyOneWeaponForSale())
            {
                EventManager.ToggleShopNextButton();
            }
        }

        private void UpdateShownWeapon()
        {
            if (_shownWeapon != null)
            {
                Destroy(_shownWeapon);
            }
            
            _shownWeapon = Instantiate(_weaponInteractables[_shownWeaponIndex], _weaponInteractableHolder.transform);
        }

        public void NextWeapon()
        {
            ++_shownWeaponIndex;
            UpdateShownWeapon();

            if (_shownWeaponIndex == (_weaponsForSale.Count - 1))
            {
                EventManager.ToggleShopNextButton();
            }
            
            EventManager.ToggleShopPreviousButton();
        }

        public void PreviousWeapon()
        {
            --_shownWeaponIndex;
            UpdateShownWeapon();

            if (_shownWeaponIndex == 0)
            {
                EventManager.ToggleShopPreviousButton();
            }
        }

        private bool OnlyOneWeaponForSale()
        {
            return _weaponsForSale.Count == 1;
        }
    }
}
