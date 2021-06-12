using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Shop
{
    public class ShopButtons : MonoBehaviour
    {
        public void ShowShopButton()
        {
            if (!UIManager.Instance.IsShowingShop)
            {
                EventManager.ShowShop();
            }
            else
            {
                EventManager.CloseShop();
            }
        }
    }
}
