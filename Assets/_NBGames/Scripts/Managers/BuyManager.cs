using System;
using System.Collections.Generic;
using _NBGames.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _NBGames.Scripts.Managers
{
    public class BuyManager : MonoBehaviour
    {
        private static BuyManager _instance;
        
        [Header("Data & Objects")]
        [SerializeField] private List<ItemForPurchase> _weaponsForSale = new List<ItemForPurchase>();
        [SerializeField] private List<GameObject> _weaponInteractables = new List<GameObject>();
        [SerializeField] private GameObject _weaponInteractableHolder;
        
        [Header("Text & Canvas")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _weaponNameTextObject;
        [SerializeField] private GameObject _weaponDescriptionTextObject;
        [SerializeField] private GameObject _purchasedTextObject;
        [SerializeField] private GameObject _insufficientFundsTextObject;

        private TextMeshProUGUI _weaponNameText, _weaponDescriptionText, _purchasedText, _insufficientFundsText;

        private int _shownWeaponIndex;
        private GameObject _shownWeapon;
        

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += ShowWeaponsForPurchase;
            EventManager.onEnableInsufficientFundsText += EnableInsufficientFundsText;
            EventManager.onEnablePurchasedText += EnablePurchasedText;
            EventManager.onShowWeaponInfo += ShowWeaponInfo;
            EventManager.onBackButtonClicked += CloseBuyOptions;
            SceneManager.activeSceneChanged += SceneChanged;
        }
        
        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= ShowWeaponsForPurchase;
            EventManager.onEnableInsufficientFundsText -= EnableInsufficientFundsText;
            EventManager.onEnablePurchasedText -= EnablePurchasedText;
            EventManager.onShowWeaponInfo -= ShowWeaponInfo;
            EventManager.onBackButtonClicked -= CloseBuyOptions;
            SceneManager.activeSceneChanged -= SceneChanged;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.Log("BuyManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
            
            if (_weaponsForSale.Count != _weaponInteractables.Count)
            {
                Debug.LogError("WeaponsForSale and WeaponInteractables size mismatch!");
            }

            CacheTextComponents();
        }

        private void CacheTextComponents()
        {
            _weaponNameText = _weaponNameTextObject.GetComponent<TextMeshProUGUI>();
            _weaponDescriptionText = _weaponDescriptionTextObject.GetComponent<TextMeshProUGUI>();
            _purchasedText = _purchasedTextObject.GetComponent<TextMeshProUGUI>();
            _insufficientFundsText = _insufficientFundsTextObject.GetComponent<TextMeshProUGUI>();

            if (_weaponNameText == null)
            {
                Debug.LogError("weaponNameText is null on " + gameObject.name);
            }

            if (_weaponDescriptionText == null)
            {
                Debug.LogError("weaponDescriptionText is null on " + gameObject.name);
            }

            if (_purchasedText == null)
            {
                Debug.LogError("purchasedText is null on " + gameObject.name);
            }

            if (_insufficientFundsText == null)
            {
                Debug.LogError("insufficientFundsText is null on " + gameObject.name);
            }
        }

        private void ShowWeaponsForPurchase()
        {
            ToggleButtonsAtSetup();
            UpdateShownWeapon();
        }

        private void ToggleButtonsAtSetup()
        {
            if (!OnlyOneWeaponForSale())
            {
                EventManager.EnableShopNextButton();
            }
            
            EnableCanvas();
        }

        private void UpdateShownWeapon()
        {
            if (_shownWeapon != null)
            {
                Destroy(_shownWeapon);
            }
            
            _shownWeapon = Instantiate(_weaponInteractables[_shownWeaponIndex], _weaponInteractableHolder.transform);
        }

        public void NextWeapon()
        {
            DisablePurchasedText();
            DisableInsufficientFundsText();
            
            ++_shownWeaponIndex;
            UpdateShownWeapon();

            if (_shownWeaponIndex == (_weaponsForSale.Count - 1))
            {
                EventManager.DisableShopNextButton();
            }
            
            EventManager.EnableShopPreviousButton();
        }

        public void PreviousWeapon()
        {
            DisablePurchasedText();
            DisableInsufficientFundsText();
            
            --_shownWeaponIndex;
            UpdateShownWeapon();

            if (_shownWeaponIndex != 0) return;
            EventManager.DisableShopPreviousButton();
            EventManager.EnableShopNextButton();
        }

        private bool OnlyOneWeaponForSale()
        {
            return _weaponsForSale.Count == 1;
        }
        
        private void EnableCanvas()
        {
            _canvas.enabled = true;
        }

        private void DisableCanvas()
        {
            _canvas.enabled = false;
        }
        
        private void ShowWeaponInfo(string itemName, string description, int value)
        {
            _weaponNameText.text = _purchasedTextObject.activeInHierarchy ? itemName : $"{itemName} - {value}";
            _weaponDescriptionText.text = description;
        }
        
        private void EnableInsufficientFundsText()
        {
            _insufficientFundsTextObject.SetActive(true);
        }
        
        private void EnablePurchasedText()
        {
            _purchasedTextObject.SetActive(true);
        }

        private void DisableInsufficientFundsText()
        {
            _insufficientFundsTextObject.SetActive(false);
        }

        private void DisablePurchasedText()
        {
            _purchasedTextObject.SetActive(false);
        }
        
        private void CloseBuyOptions()
        {
            if (_shownWeapon != null)
            {
                Destroy(_shownWeapon);
            }

            _shownWeaponIndex = 0;
            DisablePurchasedText();
            DisableInsufficientFundsText();
            EventManager.ResetNavigationButtons();
            _canvas.enabled = false;
        }
        
        private void SceneChanged(Scene oldScene, Scene currentScene)
        {
            CloseBuyOptions();
        }
    }
}
