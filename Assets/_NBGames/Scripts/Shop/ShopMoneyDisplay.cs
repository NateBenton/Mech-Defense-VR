using System;
using _NBGames.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class ShopMoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyValueText;

        private void OnEnable()
        {
            EventManager.onShowShop += UpdateMoneyText;
        }

        private void OnDisable()
        {
            EventManager.onShowShop -= UpdateMoneyText;
        }

        private void UpdateMoneyText()
        {
            _moneyValueText.text = GameManager.Instance.CurrentMoney.ToString();
        }
    }
}
