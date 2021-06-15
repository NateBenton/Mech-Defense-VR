using System.Collections.Generic;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Shop;
using TMPro;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private WeaponUpgrade[] _weaponUpgrades;
        [SerializeField] private GameObject[] _upgradeInteractables;
        [SerializeField] private GameObject _upgradeInteractableHolder;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _currentLevelObject;
        [SerializeField] private GameObject _upgradeLevelObject;
        [SerializeField] private GameObject _insufficientFundsObject;
        [SerializeField] private GameObject _noWeaponsToUpgradeObject;

        private TextMeshProUGUI _currentLevelText, _upgradeLevelText;
        private GameObject _shownUpgrade;
        private int _shownUpgradeIndex;
        private List<WeaponUpgrade> _adjustedWeaponUpgrades = new List<WeaponUpgrade>();
        
        private void OnEnable()
        {
            EventManager.onShowUpgradeOptions += ShowUpgrades;
            EventManager.onShowUpgradeOptions += EnableCanvas;
            EventManager.onBackButtonClicked += CloseUpgradeOptions;
        }
        
        private void OnDisable()
        {
            EventManager.onShowUpgradeOptions -= ShowUpgrades;
            EventManager.onShowUpgradeOptions -= EnableCanvas;
            EventManager.onBackButtonClicked -= CloseUpgradeOptions;
        }
        
        private void Awake()
        {
            if (_weaponUpgrades.Length != _upgradeInteractables.Length)
            {
                Debug.LogError("WeaponUpgrades and UpgradeInteractables size mismatch!");
            }

            CacheTextComponents();
        }

        private void CacheTextComponents()
        {
            _currentLevelText = _currentLevelObject.GetComponent<TextMeshProUGUI>();
            _upgradeLevelText = _upgradeLevelObject.GetComponent<TextMeshProUGUI>();
            
            if (_currentLevelText == null)
            {
                Debug.LogError("currentLevelText is null on " + gameObject.name);
            }
            
            if (_upgradeLevelText == null)
            {
                Debug.LogError("upgradeLevelText is null on " + gameObject.name);
            }
        }
        
        private void ShowUpgrades()
        {
            AdjustUpgradesShown();
            
            if (UpgradesExist())
            {
                ToggleButtonsAtSetup();
                UpdateShownUpgrade();
            }
            else
            {
                EnableNoWeaponsMessage();
                EventManager.DisableUpgradeNavigation();
            }
        }

        private void AdjustUpgradesShown()
        {
            foreach (var upgrade in _weaponUpgrades)
            {
                if (InventoryManager.Instance.ItemExists(upgrade.AssociatedItem))
                {
                    _adjustedWeaponUpgrades.Add(upgrade);
                }
            }
        }

        private void ToggleButtonsAtSetup()
        {
            if (!OnlyOneUpgrade())
            {
                EventManager.EnableUpgradeNextButton();
            }
        }

        private bool OnlyOneUpgrade()
        {
            return _adjustedWeaponUpgrades.Count == 1;
        }

        private bool UpgradesExist()
        {
            return _adjustedWeaponUpgrades.Count > 0;
        }

        private void EnableNoWeaponsMessage()
        {
            _noWeaponsToUpgradeObject.SetActive(true);
        }
        
        private void DisableNoWeaponsMessage()
        {
            _noWeaponsToUpgradeObject.SetActive(false);
        }
        
        private void UpdateShownUpgrade()
        {
            if (_shownUpgrade != null)
            {
                Destroy(_shownUpgrade);
            }
            
            _shownUpgrade = Instantiate(_upgradeInteractables[_shownUpgradeIndex], _upgradeInteractableHolder.transform);
        }
        
        public void NextUpgrade()
        {
            DisableInsufficientFundsText();
            
            ++_shownUpgradeIndex;
            UpdateShownUpgrade();

            if (_shownUpgradeIndex == (_adjustedWeaponUpgrades.Count - 1))
            {
                EventManager.DisableUpgradeNextButton();
            }
            
            EventManager.EnableUpgradePreviousButton();
        }
        
        public void PreviousUpgrade()
        {
            DisableInsufficientFundsText();
            
            --_shownUpgradeIndex;
            UpdateShownUpgrade();

            if (_shownUpgradeIndex != 0) return;
            EventManager.DisableUpgradePreviousButton();
            EventManager.EnableUpgradeNextButton();
        }
        
        private void EnableCanvas()
        {
            _canvas.enabled = true;
        }
        
        private void DisableInsufficientFundsText()
        {
            _insufficientFundsObject.SetActive(false);
        }
        
        private void CloseUpgradeOptions()
        {
            if (_shownUpgrade != null)
            {
                Destroy(_shownUpgrade);
            }

            _shownUpgradeIndex = 0;
            DisableInsufficientFundsText();
            EventManager.ResetNavigationButtons();
            _adjustedWeaponUpgrades.Clear();
            DisableNoWeaponsMessage();
            _canvas.enabled = false;
        }
    }
}
