using System;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Shop
{
    public class UpgradeInteractable : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(UpgradeWeapon);
        }
        
        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(UpgradeWeapon);
        }

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();

            if (_grabInteractable == null)
            {
                Debug.LogError("XRGRabInteractable is null on " + gameObject.name);
            }

            DisableInteractionByDefault();
            //CheckAvailableFunds();
        }

        private void DisableInteractionByDefault()
        {
            _grabInteractable.enabled = false;
        }
        
        private void UpgradeWeapon(SelectEnterEventArgs arg0)
        {
            throw new NotImplementedException();
        }
    }
}
