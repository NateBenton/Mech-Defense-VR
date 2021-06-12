using System;
using _NBGames.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Weapons
{
    public class Gun : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private XRGrabInteractable _grabInteractable;
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private LayerMask _damageableLayerMask;
        
        [Header("Gun Setup")]
        [SerializeField] private float _baseDamage = 2.8f;

        [Header("Haptic Feedback")]
        [SerializeField] private float _amplitude;
        [SerializeField] private float _duration;

        private float _currentDamage;

        private void OnEnable()
        {
            _grabInteractable.activated.AddListener(TriggerPulled);
        }

        private void OnDisable()
        {
            _grabInteractable.activated.RemoveListener(TriggerPulled);
        }

        private void Awake()
        {
            _currentDamage = _baseDamage;
        }

        private void TriggerPulled(ActivateEventArgs arg0)
        {
            HapticFeedback(arg0);
            CheckForTargetHit();
        }

        private void HapticFeedback(ActivateEventArgs arg0)
        {
            arg0.interactor.GetComponent<XRBaseController>().SendHapticImpulse(_amplitude, _duration);
        }

        private void CheckForTargetHit()
        {
            RaycastHit hit;
            
            // Check if raycast hit damageable target
            if (Physics.Raycast(_raycastOrigin.position, _raycastOrigin.TransformDirection(Vector3.forward), 
                out hit, Mathf.Infinity, _damageableLayerMask))
            {
                var damageable =  hit.transform.GetComponent<IDamageable>();
                damageable?.Damage(_currentDamage);
            }
        }
    }
}
