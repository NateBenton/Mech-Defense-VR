using UnityEngine;
using UnityEngine.InputSystem;

namespace _NBGames.Scripts.Misc
{
    public class InteractorSwap : MonoBehaviour
    {
        [SerializeField] private InputActionReference _aButtonAction;

        [Header("Controllers")] 
        [SerializeField] private GameObject _leftControllerDirect;
        [SerializeField] private GameObject _rightControllerDirect;
        [SerializeField] private GameObject _leftControllerRay;
        [SerializeField] private GameObject _rightControllerRay;

        private bool _isDirectInteraction;

        private void OnEnable()
        {
            _aButtonAction.action.performed += AButtonPressed;
        }

        private void OnDisable()
        {
            _aButtonAction.action.performed -= AButtonPressed;
        }

        private void AButtonPressed(InputAction.CallbackContext obj)
        {
            if (_isDirectInteraction)
            {
                _isDirectInteraction = false;
            
                _leftControllerDirect.SetActive(false);
                _rightControllerDirect.SetActive(false);
                _leftControllerRay.SetActive(true);
                _rightControllerRay.SetActive(true);
            }
            else
            {
                _isDirectInteraction = true;
            
                _leftControllerDirect.SetActive(true);
                _rightControllerDirect.SetActive(true);
                _leftControllerRay.SetActive(false);
                _rightControllerRay.SetActive(false);
            }
        }
    }
}
