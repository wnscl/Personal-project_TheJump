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


    public void Awake()
    {
        //inventory.onItemAddEvent += GiveItemToPlayer;

        for (int i = 0; i < itemDatas.Length; i++)
        {
            itemBook.Add(itemDatas[i].ItemCode, itemDatas[i]);
        }
    }


    public void MakeItem(int itemCode)
    {
        if (itemBook.TryGetValue(itemCode, out ItemData itemData))
        {

            //newItem.AddComponent<Item>();

            //inventory.GetItemToInventory(newItem);
            
        }
        else
        {
            throw new System.Exception("아이템버그발생");
        }

    }
    public void SelectItemType<T>(string type)
    //제네릭으로 만들기
    {
        
    }
}
