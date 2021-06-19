using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _NBGames.Scripts.Enemies;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Misc
{
    public class JumpSpot : MonoBehaviour
    {
        [SerializeField] private List<EnemyWave> _enemyWaves = new List<EnemyWave>();
        [SerializeField] private float _teleportDelayTime = 1f;
        [SerializeField] private bool _isLevelEnd;
        private int _waveIndex, _enemyIndex, _enemiesInWave, _enemyDeadCount;
        private bool _enemyIsSpawning;
        private float _spawnDelay, _waveDelay;

        private void OnEnable()
        {
            EventManager.onEnemyKilled += RemoveEnemy;
        }

        private void OnDisable()
        {
            EventManager.onEnemyKilled -= RemoveEnemy;
        }

        private void Start()
        {
            GetEnemyCount();
        }

        private void GetEnemyCount()
        {
            if (!_enemyWaves.Any() || !_enemyWaves[_waveIndex].Enemies.Any()) return;
            _enemiesInWave = _enemyWaves[_waveIndex].Enemies.Count;
        }

        private void Update()
        {
            if (_isLevelEnd)
            {
                Debug.Log("Level completed!");
            }
            if (_enemyIndex == _enemiesInWave || _enemyIsSpawning) return;
            GetSpawnDelay();
            StartCoroutine(SpawnEnemy(_spawnDelay));
            
        }

        private void GetSpawnDelay()
        {
            _spawnDelay = _enemyWaves[_waveIndex].SpawnDelay;
        }

        private IEnumerator SpawnEnemy(float delay)
        {
            _enemyIsSpawning = true;
            yield return new WaitForSeconds(delay);
            
            _enemyWaves[_waveIndex].Enemies[_enemyIndex].SetActive(true);

            _enemyIndex++;
            _enemyIsSpawning = false;
        }

        private void RemoveEnemy(GameObject enemy)
        {
            _enemyDeadCount++;
            StartCoroutine(CheckEnemyCount());
        }

        private IEnumerator CheckEnemyCount()
        {
            if (_enemyDeadCount != _enemiesInWave) yield break;

            if (_waveIndex + 1 < _enemyWaves.Count)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                yield return new WaitForSeconds(_teleportDelayTime);
                EventManager.AllCurrentEnemiesKilled();
            }
        }

        private IEnumerator StartNextWave()
        {
            _waveDelay = _enemyWaves[_waveIndex].NextWaveDelay;
            yield return new WaitForSeconds(_waveDelay);
            _enemyIndex = 0;
            _enemyDeadCount = 0;
            _waveIndex++;
            GetEnemyCount();
        }
    }
}
