using System;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _navigationButtons;
        [SerializeField] private BuyManager _buyManager;

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += EnableNavigationButtons;
        }

        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= EnableNavigationButtons;
        }

        private void EnableNavigationButtons()
        {
            foreach (var button in _navigationButtons)
            {
                button.SetActive(true);
            }
        }
    }
}
