using System;
using _NBGames.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class ShopMoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyValueText;

        private void Awake()
        {
            UpdateMoneyText();
        }

        private void OnEnable()
        {
            EventManager.onRemoveMoney += UpdateMoneyText;
        }

        private void OnDisable()
        {
            EventManager.onRemoveMoney -= UpdateMoneyText;
        }

        private void UpdateMoneyText()
        {
            _moneyValueText.text = GameManager.Instance.CurrentMoney.ToString();
        }

        private void UpdateMoneyText(int money)
        {
            _moneyValueText.text = GameManager.Instance.CurrentMoney.ToString();
        }
    }
}
