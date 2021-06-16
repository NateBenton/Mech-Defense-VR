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
        [SerializeField] private GameObject _currentDamageAmountObject;
        [SerializeField] private GameObject _upgradeDamageObject;
        [SerializeField] private GameObject _upgradeCostObject;
        [SerializeField] private GameObject _insufficientFundsObject;
        [SerializeField] private GameObject _noWeaponsToUpgradeObject;
        [SerializeField] private GameObject _weaponNameObject;
        [SerializeField] private GameObject _damageUpgradeObject;
        [SerializeField] private GameObject _powerUpgradeObject;
        [SerializeField] private GameObject _allUpgradedObject;
        
        private TextMeshProUGUI _currentDamageAmountText, _upgradeDamageText, _upgradeCostText, _weaponNameText;
        private GameObject _shownUpgrade;
        private int _shownUpgradeIndex;
        private List<WeaponUpgrade> _adjustedWeaponUpgrades = new List<WeaponUpgrade>();
        private float _newDamageAmount, _currentDamageAmount;
        private int _upgradeCost, _latestUpgradeIndex, _currentUpgradeIndex;
        private string _weaponName;
        private bool _allUpgraded;

        private void OnEnable()
        {
            EventManager.onShowUpgradeOptions += ShowUpgrades;
            EventManager.onShowUpgradeOptions += EnableCanvas;
            EventManager.onBackButtonClicked += CloseUpgradeOptions;
            EventManager.onUpgradeWeapon += UpgradeWeapon;
        }
        
        private void OnDisable()
        {
            EventManager.onShowUpgradeOptions -= ShowUpgrades;
            EventManager.onShowUpgradeOptions -= EnableCanvas;
            EventManager.onBackButtonClicked -= CloseUpgradeOptions;
            EventManager.onUpgradeWeapon -= UpgradeWeapon;
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
            _currentDamageAmountText = _currentDamageAmountObject.GetComponent<TextMeshProUGUI>();
            _upgradeDamageText = _upgradeDamageObject.GetComponent<TextMeshProUGUI>();
            _upgradeCostText = _upgradeCostObject.GetComponent<TextMeshProUGUI>();
            _weaponNameText = _weaponNameObject.GetComponent<TextMeshProUGUI>();
            
            if (_currentDamageAmountText == null)
            {
                Debug.LogError("currentLevelText is null on " + gameObject.name);
            }
            
            if (_upgradeDamageText == null)
            {
                Debug.LogError("upgradeLevelText is null on " + gameObject.name);
            }
            
            if (_upgradeCostText == null)
            {
                Debug.LogError("upgradeCostText is null on " + gameObject.name);
            }
            
            if (_weaponNameText == null)
            {
                Debug.LogError("weaponNameText is null on " + gameObject.name);
            }
        }
        
        private void ShowUpgrades()
        {
            AdjustUpgradesShown();
            
            if (UpgradesExist())
            {
                EnableTextObjects();
                ToggleButtonsAtSetup();
                UpdateShownUpgrade();
            }
            else
            {
                EnableNoWeaponsMessage();
                DisableTextObjects();
                EventManager.DisableUpgradeNavigation();
            }
        }

        private void EnableTextObjects()
        {
            _upgradeCostObject.SetActive(true);
            _upgradeDamageObject.SetActive(true);
            _weaponNameObject.SetActive(true);
            _currentDamageAmountObject.SetActive(true);
            _damageUpgradeObject.SetActive(true);
            _powerUpgradeObject.SetActive(true);
        }

        private void DisableTextObjects()
        {
            _upgradeCostObject.SetActive(false);
            _upgradeDamageObject.SetActive(false);
            _weaponNameObject.SetActive(false);
            _currentDamageAmountObject.SetActive(false);
            _damageUpgradeObject.SetActive(false);
            _powerUpgradeObject.SetActive(false);
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
            PopulateTextFields();

            if (_allUpgraded)
            {
                EventManager.WeaponFullyUpgraded();
            }
            else
            {
                CheckAvailableFunds();
            }
        }
        
        private void PopulateTextFields()
        {
            GetValuesForText();
            _upgradeDamageText.text = _newDamageAmount.ToString();
            _upgradeCostText.text = $"Cost: {_upgradeCost}";
            _currentDamageAmountText.text = _currentDamageAmount.ToString();
            _weaponNameText.text = _weaponName;
        }

        private void GetValuesForText()
        {
            _allUpgraded = true;
            var weaponUpgrades = _weaponUpgrades[_shownUpgradeIndex].WeaponUpgrades;
            
            for (var i = 0; i < weaponUpgrades.Length; i++)
            {
                if (weaponUpgrades[i].IsUnlocked)
                {
                    _currentUpgradeIndex = i;
                    continue;
                }
                
                _latestUpgradeIndex = i;

                _upgradeCost = weaponUpgrades[i].Cost;
                _newDamageAmount = weaponUpgrades[i].NewDamageAmount;
                _currentDamageAmount = weaponUpgrades[_currentUpgradeIndex].NewDamageAmount;
                _weaponName = _weaponUpgrades[_shownUpgradeIndex].AssociatedItem.ItemName;
                _allUpgraded = false;
                break;
            }

            if (!_allUpgraded) return;
            DisableTextObjects();
            _allUpgradedObject.SetActive(true);
        }

        private void CheckAvailableFunds()
        {
            if (GameManager.Instance.CurrentMoney >= _upgradeCost)
            {
                EventManager.MakeUpgradePurchasable();
            }
            else
            {
                _insufficientFundsObject.SetActive(true);
            }
        }
        
        public void NextUpgrade()
        {
            DisableCircumstantialText();
            EnableTextObjects();
            
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
            DisableCircumstantialText();
            EnableTextObjects();
            
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
        
        private void DisableCircumstantialText()
        {
            _insufficientFundsObject.SetActive(false);
            _allUpgradedObject.SetActive(false);
        }
        
        private void CloseUpgradeOptions()
        {
            if (_shownUpgrade != null)
            {
                Destroy(_shownUpgrade);
            }

            _shownUpgradeIndex = 0;
            DisableCircumstantialText();
            EventManager.ResetNavigationButtons();
            _adjustedWeaponUpgrades.Clear();
            DisableNoWeaponsMessage();
            _canvas.enabled = false;
        }

        private void UpgradeWeapon()
        {
            var weaponUpgrades = _weaponUpgrades[_shownUpgradeIndex].WeaponUpgrades;

            weaponUpgrades[_latestUpgradeIndex].IsUnlocked = true;
            //WeaponManager.Instance.
            EventManager.RemoveMoney(_upgradeCost);

            PopulateTextFields();

            if (_allUpgraded)
            {
                EventManager.WeaponFullyUpgraded();
            }
            else
            {
                CheckAvailableFunds();
            }
        }
    }
}
