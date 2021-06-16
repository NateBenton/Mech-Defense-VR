using System;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _NBGames.Scripts.Misc
{
    public class DisableUntilReleased : MonoBehaviour
    {
        [SerializeField] private InputActionReference _gripAction;

        private void OnEnable()
        {
            _gripAction.action.canceled += GripReleased;
        }

        private void OnDisable()
        {
            _gripAction.action.canceled -= GripReleased;
        }

        private void GripReleased(InputAction.CallbackContext obj)
        {
            EventManager.GripReleased();
        }
    }
}
