using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Misc;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class WeaponManager : MonoBehaviour
    {
        private static WeaponManager _instance;
    
        [SerializeField] private WeaponHolder[] _weaponHolders;
        [SerializeField] private float[] _damageAmounts;

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
                Debug.LogError("WeaponManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            EnableWeapons();
        }

        private void EnableWeapons()
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
    }
}
