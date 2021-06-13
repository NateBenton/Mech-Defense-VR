using System.Collections;
using System.Collections.Generic;
using _NBGames.Scripts.Managers;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager _instance;

    [Header("Weapon Holders")] 
    [SerializeField] private GameObject _primaryPistolHolder;
    [SerializeField] private GameObject _secondaryPistolHolder;
    [SerializeField] private GameObject _shotgunHolder;

    [Header("Unlocked Weapons")] 
    [SerializeField] private bool _hasPrimaryPistol = true;
    [SerializeField] private bool _hasSecondaryPistol = true;
    [SerializeField] private bool _hasShotgun = true;

    public bool HasPrimaryPistol => _hasPrimaryPistol;
    public bool HasSecondaryPistol => _hasSecondaryPistol;
    public bool HasShotgun => _hasShotgun;

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
        
        EnableDefaultWeapons();
    }

    private void EnableDefaultWeapons()
    {
        if (_hasPrimaryPistol)
        {
            _primaryPistolHolder.SetActive(true);
        }

        if (_hasSecondaryPistol)
        {
            _secondaryPistolHolder.SetActive(true);
        }

        if (_hasShotgun)
        {
            _shotgunHolder.SetActive(true);
        }
    }
}
