using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _NBGames.Scripts.Shop
{
    public class ShopButtons : MonoBehaviour
    {
        public void BuyWindowButton()
        {
            EventManager.ShowBuyOptions();
        }

        public void UpgradeWindowButton()
        {
            
        }
    }
}
