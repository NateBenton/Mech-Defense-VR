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

        private TextMeshProUGUI _currentLevelText, _upgradeLevelText;
        private GameObject _shownUpgrade;
        private int _shownUpgradeIndex;
        
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
            ToggleButtonsAtSetup();
            UpdateShownUpgrade();
        }

        private void ToggleButtonsAtSetup()
        {
            EventManager.ToggleUpgradePreviousButton();
            if (OnlyOneUpgrade())
            {
                EventManager.ToggleUpgradeNextButton();
            }
        }

        private bool OnlyOneUpgrade()
        {
            return _weaponUpgrades.Length == 1;
        }
        
        private void UpdateShownUpgrade()
        {
            if (_shownUpgrade != null)
            {
                Destroy(_shownUpgrade);
            }
            
            _shownUpgrade = Instantiate(_upgradeInteractables[_shownUpgradeIndex], _upgradeInteractableHolder.transform);
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
            _canvas.enabled = false;
        }
    }
}
