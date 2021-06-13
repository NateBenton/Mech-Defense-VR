using System;
using System.Collections;
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

        [Header("Gun Holder")] 
        [SerializeField] private Transform _gunHolder;

        private float _currentDamage;
        private Rigidbody _rigidbody;
        private Quaternion _defaultRotation;

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
            
            _rigidbody = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;

            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody somehow missing on " + gameObject.name);
            }
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
            // Check if raycast hit damageable target
            if (!Physics.Raycast(_raycastOrigin.position, _raycastOrigin.TransformDirection(Vector3.forward),
                out var hit, Mathf.Infinity, _damageableLayerMask)) return;
            
            var damageable =  hit.transform.GetComponent<IDamageable>();
            damageable?.TakeDamage(_currentDamage);
        }

        private void OnCollisionEnter(Collision other)
        {
            StartCoroutine(RespawnWeapon());
        }

        private IEnumerator RespawnWeapon()
        {
            yield return new WaitForSeconds(0.5f);
            _rigidbody.useGravity = false;
            
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.position = _gunHolder.position;
            transform.rotation = _defaultRotation;
        }
    }
}
