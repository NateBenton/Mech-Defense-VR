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
        [SerializeField] private float _health = 4f;
        [SerializeField] private Transform _targetHolder;
        [SerializeField] private Transform _target;
        public float Health { get; set; }

        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            if (_navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent null on " + gameObject.name);
            }
        }

        private void Start()
        {
            Health = _health;
            DetachTarget();
            _navMeshAgent.SetDestination(_target.position);
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
    }
}
