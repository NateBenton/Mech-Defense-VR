using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Misc
{
    public class TreasureCollection : MonoBehaviour
    {
        [SerializeField] private GlobalEnums.TreasureType _treasureType = GlobalEnums.TreasureType.Money;
        [SerializeField] private int _value;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("TreasureCollider")) return;
            EventManager.AddMoney(_value);
            Destroy(gameObject);
        }
    }
}
