using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Shop
{
    public class ShopButtons : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _sellButton;

        public void BuyWindowButton()
        {
            _buyButton.interactable = false;
            _upgradeButton.interactable = true;
            _sellButton.interactable = true;
            
            EventManager.ShowBuyOptions();
        }
    }
}
