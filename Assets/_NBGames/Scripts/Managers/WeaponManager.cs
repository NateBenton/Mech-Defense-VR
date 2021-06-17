using System;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _NBGames.Scripts.Managers
{
    public class WeaponManager : MonoBehaviour
    {
        private static WeaponManager _instance;
    
        [SerializeField] private WeaponHolder[] _weaponHolders;
        [SerializeField] private float[] _damageAmounts;
        [SerializeField] private GameObject _weaponHolderRoot;
        [SerializeField] private Item _pistolItem;

        public float[] DamageAmounts => _damageAmounts;
    
        public static WeaponManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("WeaponManager is null!");
                }

                return _instance;
            }
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
                Debug.Log("WeaponManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += DisableHolderIfShop;
        }
        
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= DisableHolderIfShop;
        }

        private void Start()
        {
            EnableWeapons();
        }

        public void EnableWeapons()
        {
            foreach (var weaponHolder in _weaponHolders)
            {
                if (InventoryManager.Instance.ItemExists(weaponHolder.AssociatedItem))
                {
                    weaponHolder.WeaponHolderObject.SetActive(true);
                }
            }
        }

        public void UpgradeWeaponDamage(int index, float newDamageAmount)
        {
            _damageAmounts[index] = newDamageAmount;
            EventManager.RefreshWeaponDamage();
        }

        public float GetDamageAmount(Item associatedItem)
        {
            for (var i = 0; i < _weaponHolders.Length; i++)
            {
                if (_weaponHolders[i].AssociatedItem != associatedItem) continue;
                return _damageAmounts[i];
            }
        
            Debug.LogError("Item mismatch in WeaponHolder/Inventory and Gun!");
            return 0.0f;
        }
        
        private void DisableHolderIfShop(Scene oldScene, Scene currentScene)
        {
            _weaponHolderRoot.SetActive(currentScene.name != "TestShop");
        }
    }
}
