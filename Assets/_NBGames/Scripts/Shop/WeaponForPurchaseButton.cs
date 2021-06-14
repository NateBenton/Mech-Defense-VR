using _NBGames.Scripts.Inventory;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class WeaponForPurchaseButton : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gunModels;
        public GameObject[] GunModels => _gunModels;
    }
}
