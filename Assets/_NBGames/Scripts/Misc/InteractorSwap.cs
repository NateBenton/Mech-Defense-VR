using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _NBGames.Scripts.Misc
{
    public class InteractorSwap : MonoBehaviour
    {
        [SerializeField] private InputActionReference _aButtonAction;

        [Header("Controllers")]
        [SerializeField] private GameObject _rightControllerDirect;
        [SerializeField] private GameObject _rightControllerRay;

        private bool _isDirect = true;
        
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
            if (_isDirect)
            {
                _rightControllerDirect.SetActive(false);
                _rightControllerRay.SetActive(true);
            }
            else
            {
                _rightControllerDirect.SetActive(true);
                _rightControllerRay.SetActive(false);
            }
        }
    }
}
