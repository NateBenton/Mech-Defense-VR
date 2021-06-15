using System;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Shop
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _nextButtonObject;
        [SerializeField] private GameObject _previousButtonObject;

        private Button _nextButton, _previousButton;

        private void Awake()
        {
            _nextButton = _nextButtonObject.GetComponent<Button>();
            _previousButton = _previousButtonObject.GetComponent<Button>();

            if (_nextButton == null)
            {
                Debug.LogError("nextButton is null on " + gameObject.name);
            }

            if (_previousButton == null)
            {
                Debug.LogError("previousButton is null on " + gameObject.name);
            }
        }

        private void OnEnable()
        {
            EventManager.onShowBuyOptions += EnableNavigationButtons;
            EventManager.onToggleShopNextButton += ToggleNextButton;
            EventManager.onToggleShopPreviousButton += TogglePreviousButton;
            EventManager.onResetNavigationButtons += ResetButtons;
        }

        private void OnDisable()
        {
            EventManager.onShowBuyOptions -= EnableNavigationButtons;
            EventManager.onToggleShopNextButton -= ToggleNextButton;
            EventManager.onToggleShopPreviousButton -= TogglePreviousButton;
            EventManager.onResetNavigationButtons -= ResetButtons;
        }

        private void EnableNavigationButtons()
        {
            _nextButtonObject.SetActive(true);
            _previousButtonObject.SetActive(true);
        }
        
        private void ToggleNextButton()
        {
            _nextButton.interactable = !_nextButton.interactable;
        }
        
        private void TogglePreviousButton()
        {
            _previousButton.interactable = !_previousButton.interactable;
        }
        
        private void ResetButtons()
        {
            _nextButton.interactable = true;
            _previousButton.interactable = true;
        }
    }
}
