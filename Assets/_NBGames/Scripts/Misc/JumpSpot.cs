using System.Collections;
using System.Collections.Generic;
using _NBGames.Scripts.Enemies;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Misc
{
    public class JumpSpot : MonoBehaviour
    {
        [SerializeField] private List<EnemyWave> _enemies = new List<EnemyWave>();
        private int _waveIndex;

        private void OnEnable()
        {
            EventManager.onEnemyKilled += RemoveEnemy;
            
            EnableEnemies();
        }

        private void OnDisable()
        {
            EventManager.onEnemyKilled -= RemoveEnemy;
        }

        private void EnableEnemies()
        {
            foreach (var enemy in _enemies)
            {
                //enemy.SetActive(true);
            }
        }

        private void RemoveEnemy(GameObject enemy)
        {
            _enemies[_waveIndex].Enemies.Remove(enemy);
            StartCoroutine(CheckEnemyCount());
        }

        private IEnumerator CheckEnemyCount()
        {
            if (_enemies[_waveIndex].Enemies.Count != 0) yield break;
            yield return new WaitForSeconds(3);
            EventManager.AllCurrentEnemiesKilled();
        }
    }
}
