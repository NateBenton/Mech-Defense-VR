using System;
using System.Collections.Generic;
using _NBGames.Scripts.Shop;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _NBGames.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        
        [Header("Money Settings")]
        [SerializeField] private int _currentMoney;
        [SerializeField] private int _maxMoney = 999999;

        public int CurrentMoney => _currentMoney;

        public GameObject PlayerHitbox { get; private set; }

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
            EventManager.onRemoveMoney += RemoveMoney;
            SceneManager.activeSceneChanged += FindPlayerHitboxOnLoad;
        }

        

        private void OnDisable()
        {
            EventManager.onAddMoney -= AddMoney;
            EventManager.onRemoveMoney -= RemoveMoney;
            SceneManager.activeSceneChanged -= FindPlayerHitboxOnLoad;
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
                Debug.Log("GameManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            FindPlayerHitbox();
        }
        
        private void FindPlayerHitboxOnLoad(Scene arg0, Scene arg1)
        {
            FindPlayerHitbox();
        }

        private void FindPlayerHitbox()
        {
            PlayerHitbox = GameObject.FindWithTag("PlayerHitbox");
            if (PlayerHitbox == null)
            {
                Debug.LogError("PlayerHitbox is null on " + gameObject.name);
            }
        }

        private void AddMoney(int amount)
        {
            _currentMoney = Mathf.Min((_currentMoney + amount), _maxMoney);
        }

        private void RemoveMoney(int amount)
        {
            _currentMoney -= amount;
        }
    }
}
