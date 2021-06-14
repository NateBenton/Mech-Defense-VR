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

        private void ShowWeaponsForPurchase()
        {
            _weaponInteractableHolder.SetActive(true);
            UpdateShownWeapon();
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
            Debug.Log("Next");
        }

        public void PreviousWeapon()
        {
            Debug.Log("Previous");
        }
    }
}
