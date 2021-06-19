using System;
using System.Collections.Generic;
using UnityEngine;

namespace _NBGames.Scripts.Enemies
{
    [Serializable]
    public class EnemyWave
    {
        [Tooltip("List of enemy game objects associated with JumpSpot.")]
        public List<GameObject> Enemies = new List<GameObject>();
        [Tooltip("Delay time in seconds between each enemy spawning.")]
        public float SpawnDelay;
        [Tooltip("Delay time in seconds between the next wave starting.")]
        public float NextWaveDelay;
    }
}
