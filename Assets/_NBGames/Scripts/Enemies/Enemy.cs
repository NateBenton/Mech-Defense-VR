using System;
using _NBGames.Scripts.Interfaces;
using UnityEngine;

namespace _NBGames.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _health = 4f;
        public float Health { get; set; }

        private void Awake()
        {
            Health = _health;
        }
        
        public void TakeDamage(float damageAmount)
        {
            Health = Mathf.Max(0, Health - damageAmount);

            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
