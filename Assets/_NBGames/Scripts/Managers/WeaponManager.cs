using System.Collections;
using System.Collections.Generic;
using _NBGames.Scripts.Managers;
using _NBGames.Scripts.Misc;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager _instance;

    [Header("Weapon Holders")] 
    [SerializeField] private WeaponHolder[] _weaponHolders;
    [SerializeField] private float[] _damageAmounts;
    
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
}
