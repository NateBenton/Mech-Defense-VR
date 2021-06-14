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
            EventManager.onShowWeaponInfo += ShowWeaponInfo;
        }

        private void OnDisable()
        {
            EventManager.onShowInsufficientFundsText -= ShowInsufficientFundsText;
            EventManager.onShowPurchasedText -= ShowPurchasedText;
            EventManager.onShowWeaponInfo -= ShowWeaponInfo;
        }
        
        private void EnableCanvas()
        {
            _canvas.enabled = true;
        }
        
        private void ShowWeaponInfo(string itemName, string description, int value)
        {
            _weaponNameText.text = $"{itemName} - {value}";
            _weaponDescriptionText.text = description;
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
