using System;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class BuyWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponForPurchaseButton;
        [SerializeField] private GameObject _buttonHolder;
        private void Awake()
        {
            var weaponsForSaleCount = GameManager.Instance.WeaponsForSaleInShop.Count;
            for (var i = 0; i < weaponsForSaleCount; i++)
            {
                Instantiate(_weaponForPurchaseButton, _buttonHolder.transform);
            }
        }
    }
}
