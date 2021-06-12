using System;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        
        [Header("Money Settings")]
        [SerializeField] private int _currentMoney;
        [SerializeField] private int _maxMoney = 999999;

        public int CurrentMoney => _currentMoney;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is NULL");
                }
                
                return _instance;
            }
        }

        private void OnEnable()
        {
            EventManager.onAddMoney += AddMoney;
        }

        private void OnDisable()
        {
            EventManager.onAddMoney -= AddMoney;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.LogError("GameManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
        }

        private void AddMoney(int amount)
        {
            _currentMoney = Mathf.Min((_currentMoney + amount), _maxMoney);
        }
    }
}
