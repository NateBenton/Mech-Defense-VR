using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _NBGames.Scripts.Inventory;
using _NBGames.Scripts.Managers;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("InventoryManager is null!");
            }

            return _instance;
        }
    }

    [SerializeField] private Inventory _inventory;

    private void OnEnable()
    {
        EventManager.onAddItemToInventory += AddItemToInventory;
    }

    private void OnDisable()
    {
        EventManager.onAddItemToInventory -= AddItemToInventory;
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
            Debug.LogError("InventoryManager already exists. Destroying!");
            Destroy(this.gameObject);
        }
    }
    
    private void AddItemToInventory(Item itemToAdd, int quantity)
    {
        foreach (var itemSlot in _inventory.ItemSlots.Where(itemSlot => itemSlot.Item == itemToAdd))
        {
            itemSlot.Quantity++;
            return;
        }


        var itemSlotToAppend = new ItemSlot {Item = itemToAdd, Quantity = quantity};

        _inventory.ItemSlots.Add(itemSlotToAppend);
    }
}
