using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    public ItemData[] itemDatas;
    //아이템 데이터를 배열로 할당할 필드
    Dictionary<int, ItemData> itemBook = new Dictionary<int, ItemData>();
    public GameObject item;

    public Action<GameObject> factoryOrderEvent;

    public void Awake()
    {
        //inventory.onItemAddEvent += GiveItemToPlayer;

        for (int i = 0; i < itemDatas.Length; i++)
        {
            itemBook.Add(itemDatas[i].ItemCode, itemDatas[i]);
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown("6"))
        {
            MakeItem(0);
        }
    }


    public void MakeItem(int itemCode)
    {
        if (inventory.itemCount >= 5)
        {
            return;
        }

        Item orginItem = item.GetComponent<Item>();
        if (itemBook.TryGetValue(itemCode, out ItemData itemData))
        {
            orginItem.itemName = itemData.ItemName;
            orginItem.itemIcon = itemData.ItemIcon;
            orginItem.itemCode = itemData.ItemCode;
            orginItem.image.sprite = itemData.ItemIcon;
            orginItem.name.text = itemData.ItemName;

            GameObject newitem = Instantiate(item);
            
            inventory.TakeItem(newitem);

        }
        else
        {
            throw new System.Exception("아이템버그발생");
        }

    }
}
