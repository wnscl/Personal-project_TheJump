using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public event Action<Item> onItemAddEvent;

    private void Awake()
    {
        onItemAddEvent += GetItemToInventory;
    }

    public void GetItemToInventory(Item item)
    {
        if (inventory.Count >= 10)
        {
            return;
        }
        inventory.Add(item);
    }

}
