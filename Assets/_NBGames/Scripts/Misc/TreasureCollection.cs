using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Managers;
using UnityEngine;

namespace _NBGames.Scripts.Misc
{
    public class TreasureCollection : MonoBehaviour
    {
        [SerializeField] private GlobalEnums.TreasureType _treasureType = GlobalEnums.TreasureType.Money;
        [SerializeField] private int _quantity;
        [SerializeField] private Item _item;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("TreasureCollider")) return;

            if (_treasureType == GlobalEnums.TreasureType.Money)
            {
                EventManager.AddMoney(_quantity);
            }
            else if (_item != null)
            {
                EventManager.AddItemToInventory(_item, _quantity);
            }
            else
            {
                Debug.LogError("Item was NULL for " + gameObject.name);
            }

            Destroy(gameObject);
        }
    }
}
