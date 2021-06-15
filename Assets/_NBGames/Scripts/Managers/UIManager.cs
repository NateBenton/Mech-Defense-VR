using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainWindow;
        [SerializeField] private GameObject _backButtonObject;

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
            _backButtonObject.SetActive(true);
        }

        private void ShowMainWindow()
        {
            _mainWindow.SetActive(true);
        }

        public void BackButton()
        {
            _backButtonObject.SetActive(false);
            ShowMainWindow();
            EventManager.BackButtonClicked();
        }
    }
}
