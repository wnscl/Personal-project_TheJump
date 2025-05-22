using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] itemSlot;
    public int itemCount = 0;

    public void TakeItem(GameObject createdItem)
    {
        if (itemCount >= 5)
        {
            return;
        }

        createdItem.transform.SetParent(itemSlot[itemCount].transform, false);

        itemCount++;
    }
    public void UseAndRemoveItem(int num)
    {
        int index = num - 1;
        Item itemToUse = itemSlot[index].GetComponentInChildren<Item>();
        if (itemToUse == null || FindObjectOfType<Player>().nowCoroutine != null)
        {
            return;
        }
        ItemEffectManager.Instance.InjectItemEffect(itemToUse.itemCode);
        itemToUse.UseingItem();

        for (int i = index; i < itemCount - 1; i++)
        {
            if (itemSlot[i + 1].transform.childCount > 0)
            {
                Transform nextItem = itemSlot[i + 1].transform.GetChild(0);
                nextItem.SetParent(itemSlot[i].transform, false);
            }
        }
        itemCount--;
    }
    
}
