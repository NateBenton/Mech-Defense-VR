using System;
using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public static event Action<int> onAddMoney;

        public static void AddMoney(int amountToAdd)
        {
            onAddMoney?.Invoke(amountToAdd);
        }

        public static event Action onShowShop;

        public static void ShowShop()
        {
            onShowShop?.Invoke();
        }
    }
}
