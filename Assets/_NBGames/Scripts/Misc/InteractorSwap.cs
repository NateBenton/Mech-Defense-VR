using _NBGames.Scripts.Managers;
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

        private bool _isRightHandedMode = true;

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
            if (_isRightHandedMode)
            {
                _isRightHandedMode = false;
            
                _leftControllerDirect.SetActive(true);
                _rightControllerDirect.SetActive(false);
                _leftControllerRay.SetActive(false);
                _rightControllerRay.SetActive(true);
            }
            else
            {
                _isRightHandedMode = true;
            
                _leftControllerDirect.SetActive(false);
                _rightControllerDirect.SetActive(true);
                _leftControllerRay.SetActive(true);
                _rightControllerRay.SetActive(false);
            }
        }

        private void ChangeInteractorType()
        {
            if (_isRightHandedMode)
            {
                _leftControllerDirect.SetActive(false);
                _rightControllerDirect.SetActive(true);
                _leftControllerRay.SetActive(true);
                _rightControllerRay.SetActive(false);
            }
            else
            {
                _leftControllerDirect.SetActive(true);
                _rightControllerDirect.SetActive(false);
                _leftControllerRay.SetActive(false);
                _rightControllerRay.SetActive(true);
            }
        }
        
        private void RayInteractEnable()
        {
            _leftControllerDirect.SetActive(false);
            _rightControllerDirect.SetActive(false);
            _leftControllerRay.SetActive(true);
            _rightControllerRay.SetActive(true);
        }
    }
}
