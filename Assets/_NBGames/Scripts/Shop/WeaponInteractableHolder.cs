using System;
using _NBGames.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class WeaponInteractableHolder : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private TextMeshProUGUI _weaponDescriptionText;
        [SerializeField] private TextMeshProUGUI _purchasedText;
        [SerializeField] private TextMeshProUGUI _insufficientFundsText;

        private void OnEnable()
        {
            EventManager.onShowInsufficientFundsText += ShowInsufficientFundsText;
            EventManager.onShowPurchasedText += ShowPurchasedText;
            EventManager.onShowBuyOptions += EnableCanvas;
        }

        private void OnDisable()
        {
            EventManager.onShowInsufficientFundsText -= ShowInsufficientFundsText;
            EventManager.onShowPurchasedText -= ShowPurchasedText;
        }
        
        private void EnableCanvas()
        {
            _canvas.enabled = true;
        }

        private void ShowInsufficientFundsText()
        {
            _insufficientFundsText.gameObject.SetActive(true);
        }
        
        private void ShowPurchasedText()
        {
            _purchasedText.gameObject.SetActive(true);
        }
    }
}
