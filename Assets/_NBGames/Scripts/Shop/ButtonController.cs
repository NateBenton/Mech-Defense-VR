using System;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Shop
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _shopNextButtonObject;
        [SerializeField] private GameObject _shopPreviousButtonObject;
        [SerializeField] private GameObject _upgradeNextButtonObject;
        [SerializeField] private GameObject _upgradePreviousButtonObject;

        private Button _shopNextButton, _shopPreviousButton, _upgradeNextButton, _upgradePreviousButton;

        private void Awake()
        {
            CacheComponents();
        }

        private void CacheComponents()
        {
            _shopNextButton = _shopNextButtonObject.GetComponent<Button>();
            _shopPreviousButton = _shopPreviousButtonObject.GetComponent<Button>();
            _upgradeNextButton = _upgradeNextButtonObject.GetComponent<Button>();
            _upgradePreviousButton = _upgradePreviousButtonObject.GetComponent<Button>();

            if (_shopNextButton == null)
            {
                Debug.LogError("shopNextButton is null on " + gameObject.name);
            }

            if (_shopPreviousButton == null)
            {
                Debug.LogError("shopPreviousButton is null on " + gameObject.name);
            }

            if (_upgradePreviousButton == null)
            {
                Debug.LogError("upgradePreviousButton is null on " + gameObject.name);
            }

            if (_upgradeNextButton == null)
            {
                Debug.LogError("upgradeNextButton is null on " + gameObject.name);
            }
        }

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += EnableNavigationButtons;
            EventManager.onToggleShopNextButton += ToggleShopNextButton;
            EventManager.onToggleShopPreviousButton += ToggleShopPreviousButton;
            EventManager.onResetNavigationButtons += ResetButtons;
            EventManager.onShowUpgradeOptions += EnableUpgradeNavigation;
            EventManager.onToggleUpgradeNextButton += ToggleUpgradeNextButton;
            EventManager.onToggleUpgradePreviousButton += ToggleUpgradePreviousButton;
        }

        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= EnableNavigationButtons;
            EventManager.onToggleShopNextButton -= ToggleShopNextButton;
            EventManager.onToggleShopPreviousButton -= ToggleShopPreviousButton;
            EventManager.onResetNavigationButtons -= ResetButtons;
            EventManager.onShowUpgradeOptions -= EnableUpgradeNavigation;
            EventManager.onToggleUpgradeNextButton -= ToggleUpgradeNextButton;
            EventManager.onToggleUpgradePreviousButton -= ToggleUpgradePreviousButton;
        }

        private void EnableNavigationButtons()
        {
            _shopNextButtonObject.SetActive(true);
            _shopPreviousButtonObject.SetActive(true);
        }
        
        private void EnableUpgradeNavigation()
        {
            _upgradeNextButtonObject.SetActive(true);
            _upgradePreviousButtonObject.SetActive(true);
        }
        
        private void ToggleShopNextButton()
        {
            _shopNextButton.interactable = !_shopNextButton.interactable;
        }
        
        private void ToggleShopPreviousButton()
        {
            _shopPreviousButton.interactable = !_shopPreviousButton.interactable;
        }
        
        private void ToggleUpgradeNextButton()
        {
            _upgradeNextButton.interactable = !_upgradeNextButton.interactable;
        }
        
        private void ToggleUpgradePreviousButton()
        {
            _upgradePreviousButton.interactable = !_upgradePreviousButton.interactable;
        }
        
        private void ResetButtons()
        {
            _shopNextButton.interactable = true;
            _shopPreviousButton.interactable = true;
            _upgradeNextButton.interactable = true;
            _upgradePreviousButton.interactable = true;
        }
    }
}
