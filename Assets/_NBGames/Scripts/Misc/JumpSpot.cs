using System;
using System.Collections;
using System.Collections.Generic;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Misc
{
    public class JumpSpot : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

        private void OnEnable()
        {
            EventManager.onEnemyKilled += RemoveEnemy;
        }

        private void OnDisable()
        {
            EventManager.onEnemyKilled -= RemoveEnemy;
        }

        private void RemoveEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
            StartCoroutine(CheckEnemyCount());
        }

        private IEnumerator CheckEnemyCount()
        {
            if (_enemies.Count != 0) yield break;
            yield return new WaitForSeconds(3);
            EventManager.AllCurrentEnemiesKilled();
        }
    }
}
