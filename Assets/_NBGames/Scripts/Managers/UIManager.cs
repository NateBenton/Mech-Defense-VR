using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

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

        [SerializeField] private GameObject _shopObject;
        [SerializeField] private GameObject _mainWindow;
        [SerializeField] private GameObject _buyWindow;
        
        private bool _isShowingShop;

        public bool IsShowingShop => _isShowingShop;

        public GameObject MainWindow => _mainWindow;
        public GameObject BuyWindow => _buyWindow;

        private void OnEnable()
        {
            EventManager.onShowShop += ShowShop;
            EventManager.onCloseShop += CloseShop;
        }

        private void OnDisable()
        {
            EventManager.onShowShop -= ShowShop;
            EventManager.onCloseShop -= CloseShop;
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

        private void ShowShop()
        {
            _isShowingShop = true;
            _shopObject.SetActive(true);
        }
        
        private void CloseShop()
        {
            _isShowingShop = false;
            _shopObject.SetActive(false);
        }
    }
}
