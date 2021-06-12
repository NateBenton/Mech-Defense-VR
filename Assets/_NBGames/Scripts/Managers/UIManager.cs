using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UI Manager is null!");

                }

                return _instance;
            }
        }

        private void OnEnable()
        {
            EventManager.onShowShop += ToggleShop;
        }

        private void OnDisable()
        {
            EventManager.onShowShop -= ToggleShop;
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
                Debug.LogError("UIManager already exists. Destroying!");
                Destroy(this.gameObject);
            }
        }

        private void ToggleShop()
        {
        
        }
    }
}
