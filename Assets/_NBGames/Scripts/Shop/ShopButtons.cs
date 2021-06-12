using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class ShopButtons : MonoBehaviour
    {
        public void ShowShopButton()
        {
            EventManager.ShowShop();
        }
    }
}
