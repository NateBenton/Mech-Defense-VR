using _NBGames.Scripts.Shop;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private WeaponUpgrade[] _weaponUpgrades;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _currentLevelObject;
        [SerializeField] private GameObject _upgradeLevelObject;
        [SerializeField] private GameObject _insufficientFundsObject;
    }
}
