using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    public ItemData[] itemDatas;
    //������ �����͸� �迭�� �Ҵ��� �ʵ�
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
            throw new System.Exception("�����۹��׹߻�");
        }

    }
    public void SelectItemType<T>(string type)
    //���׸����� �����
    {
        
    }
}
