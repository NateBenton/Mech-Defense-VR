using UnityEngine;
using UnityEngine.InputSystem;

namespace _NBGames.Scripts
{
    public class HandAnimationControl : MonoBehaviour
    {
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private InputActionReference _gripAction;
        [SerializeField] private InputActionReference _triggerAction;

        private void OnEnable()
        {
            _gripAction.action.performed += GripAnimation;
            _triggerAction.action.performed += TriggerAnimation;
        }

        private void AButtonTest(InputAction.CallbackContext obj)
        {
            Debug.Log("A button pressed!");
        }

        private void OnDisable()
        {
            _gripAction.action.performed -= GripAnimation;
            _triggerAction.action.performed -= TriggerAnimation;
        }

        private void GripAnimation(InputAction.CallbackContext obj)
        {
            _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
        }
    
        private void TriggerAnimation(InputAction.CallbackContext obj)
        {
            _handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
        }
    }
}
