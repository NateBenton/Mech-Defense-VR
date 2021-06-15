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
            EventManager.onEnableShopNextButton += EnableShopNextButton;
            EventManager.onDisableShopNextButton += DisableShopNextButton;
            
            EventManager.onEnableShopPreviousButton += EnableShopPreviousButton;
            EventManager.onDisableShopPreviousButton += DisableShopPreviousButton;
            
            EventManager.onResetNavigationButtons += ResetButtons;
            
            EventManager.onEnableUpgradeNextButton += EnableUpgradeNextButton;
            EventManager.onDisableUpgradeNextButton += DisableUpgradeNextButton;
            
            EventManager.onEnableUpgradePreviousButton += EnableUpgradePreviousButton;
            EventManager.onDisableUpgradePreviousButton += DisableUpgradePreviousButton;
            
            EventManager.onDisableUpgradeNavigation += DisableUpgradeNavigation;
            
        }

        private void OnDisable()
        {
            EventManager.onEnableShopNextButton -= EnableShopNextButton;
            EventManager.onDisableShopNextButton -= DisableShopNextButton;
            
            EventManager.onEnableShopPreviousButton -= EnableShopPreviousButton;
            EventManager.onDisableShopPreviousButton -= DisableShopPreviousButton;
            
            EventManager.onResetNavigationButtons -= ResetButtons;
            
            EventManager.onEnableUpgradeNextButton -= EnableUpgradeNextButton;
            EventManager.onDisableUpgradeNextButton -= DisableUpgradeNextButton;
            
            EventManager.onEnableUpgradePreviousButton -= EnableUpgradePreviousButton;
            EventManager.onDisableUpgradePreviousButton -= DisableUpgradePreviousButton;
            
            EventManager.onDisableUpgradeNavigation -= DisableUpgradeNavigation;
        }

        private void DisableUpgradeNavigation()
        {
            _upgradeNextButton.interactable = false;
            _upgradePreviousButton.interactable = false;
        }
        
        private void EnableShopNextButton()
        {
            _shopNextButton.interactable = true;
        }
        
        private void DisableShopNextButton()
        {
            _shopNextButton.interactable = false;
        }
        
        private void EnableShopPreviousButton()
        {
            _shopPreviousButton.interactable = true;
        }

        private void DisableShopPreviousButton()
        {
            _shopPreviousButton.interactable = false;
        }
        
        private void EnableUpgradeNextButton()
        {
            _upgradeNextButton.interactable = true;
        }
        
        private void DisableUpgradeNextButton()
        {
            _upgradeNextButton.interactable = false;
        }
        
        private void EnableUpgradePreviousButton()
        {
            _upgradePreviousButton.interactable = true;
        }
        
        private void DisableUpgradePreviousButton()
        {
            _upgradePreviousButton.interactable = false;
        }
        
        private void ResetButtons()
        {
            _shopNextButton.interactable = false;
            _shopPreviousButton.interactable = false;
            _upgradeNextButton.interactable = false;
            _upgradePreviousButton.interactable = false;
        }
    }
}
