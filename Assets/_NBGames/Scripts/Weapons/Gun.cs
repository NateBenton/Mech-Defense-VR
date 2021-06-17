using System;
using System.Collections;
using _NBGames.Scripts.Interfaces;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Managers;
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
        [SerializeField] private Item _associatedItem;

        [Header("Haptic Feedback")]
        [SerializeField] private float _amplitude;
        [SerializeField] private float _duration;

        [Header("Gun Holder")] 
        [SerializeField] private Transform _gunHolder;

        [SerializeField] private float _currentDamage;
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _grabInteractable.activated.AddListener(TriggerPulled);
            EventManager.onRefreshWeaponDamage += GetDamageAmount;
            GetDamageAmount();
        }
        
        private void OnDisable()
        {
            _grabInteractable.activated.RemoveListener(TriggerPulled);
            EventManager.onRefreshWeaponDamage -= GetDamageAmount;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody somehow missing on " + gameObject.name);
            }

            if (_associatedItem == null)
            {
                Debug.LogError("associatedItem is null on " + gameObject.name);
            }
        }

        private void GetDamageAmount()
        {
            _currentDamage = WeaponManager.Instance.GetDamageAmount(_associatedItem);
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
            transform.rotation = _gunHolder.rotation;
        }
    }
}
