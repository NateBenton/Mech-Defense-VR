using System;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Shop
{
    public class ShopInteractable : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;
        [SerializeField] private Item _item;

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(PurchaseWeapon);
        }

        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(PurchaseWeapon);
        }

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();

            if (_grabInteractable == null)
            {
                Debug.LogError("XRGRabInteractable is null on " + gameObject.name);
            }
            
            if (InventoryManager.Instance.ItemExists(_item))
            {
                EventManager.ShowPurchasedText();
                Destroy(gameObject);
                return;
            }

            DisableInteractionByDefault();
            CheckAvailableFunds();
        }

        private void Start()
        {
            EventManager.ShowWeaponInfo(_item.ItemName, _item.Description, _item.PurchasePrice);
        }

        private void DisableInteractionByDefault()
        {
            _grabInteractable.enabled = false;
        }

        private void CheckAvailableFunds()
        {
            if (!InventoryManager.Instance.ItemExists(_item))
            {
                if (GameManager.Instance.CurrentMoney >= _item.PurchasePrice)
                {
                    MakeAvailableForPurchase();
                }
                else
                {
                    EventManager.ShowInsufficientFundsText();
                }
            }
            else
            {
                EventManager.ShowPurchasedText();
            }
        }

        private void MakeAvailableForPurchase()
        {
            _grabInteractable.enabled = true;
        }
        
        private void PurchaseWeapon(SelectEnterEventArgs arg0)
        {
            EventManager.RemoveMoney(_item.PurchasePrice);
            EventManager.AddItemToInventory(_item, 1);
            EventManager.ShowPurchasedText();
            _grabInteractable.enabled = false;
            
            Destroy(gameObject);
        }
    }
}
