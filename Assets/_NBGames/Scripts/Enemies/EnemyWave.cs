using System;
using System.Collections.Generic;
using UnityEngine;

namespace _NBGames.Scripts.Enemies
{
    [Serializable]
    public class EnemyWave
    {
        public List<GameObject> Enemies;
        public int WaitTimeForProceedingWave;
    }
}
