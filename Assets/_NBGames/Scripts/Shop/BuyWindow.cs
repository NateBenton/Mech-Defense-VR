using System;
using System.Collections.Generic;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class BuyWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponForPurchaseButton;
        [SerializeField] private GameObject _buttonHolder;
        [SerializeField] private List<ItemForPurchase> _weaponsForSale = new List<ItemForPurchase>();
        private void Awake()
        {
            for (var i = 0; i < _weaponsForSale.Count; i++)
            {
                var button = Instantiate(_weaponForPurchaseButton, _buttonHolder.transform);
                var weaponForPurchaseButton = button.GetComponent<WeaponForPurchaseButton>();

                if (weaponForPurchaseButton == null)
                {
                    Debug.LogError("WeaponForPurchase component is null!");
                }
                else if (weaponForPurchaseButton.GunModels.Length != _weaponsForSale.Count)
                {
                    Debug.LogError("WeaponsForSale and GunModel size mismatch!");
                }
                else
                {
                    weaponForPurchaseButton.GunModels[i].SetActive(true);
                }
            }
        }
    }
}
