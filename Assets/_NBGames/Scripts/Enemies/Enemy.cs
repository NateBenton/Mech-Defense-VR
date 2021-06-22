using System;
using _NBGames.Scripts.Interfaces;
using _NBGames.Scripts.Managers;
using _NBGames.Scripts.Misc;
using UnityEngine;
using UnityEngine.AI;

namespace _NBGames.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _pivotPoint;
        [SerializeField] private float _rotatePointSpeed = 1f;
        [SerializeField] private Transform[] _raycastOrigins;
        [SerializeField] private float _health = 4f;
        [SerializeField] private Transform _targetHolder;
        [SerializeField] private Transform _target;

        private Animator _animator;
        public float Health { get; set; }

        private NavMeshAgent _navMeshAgent;
        private bool _reachedTarget;
        private GameObject _camera;
        private bool _isAttackingPlayer;

        private void Awake()
        {
            CacheComponents();
        }

        private void CacheComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            if (_navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent null on " + gameObject.name);
            }

            if (_animator == null)
            {
                Debug.LogError("Animator null on " + gameObject.name);
            }
        }

        private void Start()
        {
            Health = _health;
            DetachTarget();
            _camera = GameManager.Instance.PlayerHitbox;
            _navMeshAgent.SetDestination(_target.position);
        }

        private void Update()
        {
            if (!_isAttackingPlayer) return;
            TurnTowardsPlayer();
            
            if (_reachedTarget) return;
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance) return;
            _animator.SetBool("isMoving", false);
            _reachedTarget = true;
            TurnTowardsPlayer();
        }

        private void TurnTowardsPlayer()
        {
            var direction = (_camera.transform.position - _pivotPoint.transform.position);
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            
            _pivotPoint.transform.rotation = Quaternion.Lerp(_pivotPoint.transform.rotation, 
                targetRotation, _rotatePointSpeed * Time.deltaTime);
        }

        private void DetachTarget()
        {
            _target.transform.parent = _targetHolder;
        }

        public void TakeDamage(float damageAmount)
        {
            Health = Mathf.Max(0, Health - damageAmount);

            if (Health != 0) return;
            DropMoney();
            EventManager.EnemyKilled(gameObject);
            Destroy(_target.gameObject);
            Destroy(gameObject);
        }

        private void DropMoney()
        {
            // pull money from pooling object later
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerAttack"))
            {
                _animator.SetBool("isShooting", true);
                _isAttackingPlayer = true;
            }
        }

        public void Shoot()
        {
            foreach (var origin in _raycastOrigins)
            {
                Debug.Log("Shot!");
                if (!Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward),
                    out var hit, Mathf.Infinity)) return;

                if (hit.transform.CompareTag("PlayerHitbox"))
                {
                    Debug.Log("You were hit!");
                }
            }
        }
    }
}
