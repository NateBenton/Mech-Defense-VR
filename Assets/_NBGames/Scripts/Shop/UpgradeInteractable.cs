using System;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Shop
{
    public class UpgradeInteractable : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;
        private Rigidbody _rigidbody;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private bool _canBeGrabbed = true;
        private bool _canBePurchased;

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(UpgradeWeapon);
            EventManager.onMakeUpgradePurchasable += MakeAvailableForPurchase;
            EventManager.onGripReleased += EnableGrabbing;
        }
        
        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(UpgradeWeapon);
            EventManager.onMakeUpgradePurchasable -= MakeAvailableForPurchase;
            EventManager.onGripReleased -= EnableGrabbing;
        }

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _rigidbody = GetComponent<Rigidbody>();

            if (_grabInteractable == null)
            {
                Debug.LogError("XRGRabInteractable is null on " + gameObject.name);
            }

            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody is null on " + gameObject.name);
            }

            _startPosition = transform.position;
            _startRotation = transform.rotation;

            DisableInteraction();
        }

        private void Update()
        {
            if (_canBePurchased && _canBeGrabbed)
            {
                _grabInteractable.enabled = true;
            }
        }

        private void DisableInteraction()
        {
            _grabInteractable.enabled = false;
        }
        
        private void MakeAvailableForPurchase()
        {
            _canBePurchased = true;
        }
        
        private void UpgradeWeapon(SelectEnterEventArgs arg0)
        {
            _grabInteractable.enabled = false;
            _canBeGrabbed = false;
            _canBePurchased = false;
            EventManager.UpgradeWeapon();
        }

        private void EnableGrabbing()
        {
            _canBeGrabbed = true;
        }
    }
}
