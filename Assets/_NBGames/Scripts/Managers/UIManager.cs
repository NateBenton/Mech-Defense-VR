using System;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainWindow;

        private bool _isShowingShop;
        public bool IsShowingShop => _isShowingShop;

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += HideMainWindow;
        }

        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= HideMainWindow;
        }

        private void HideMainWindow()
        {
            _mainWindow.SetActive(false);
        }
    }
}
