using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{

}

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField] private int itemCode;
    public int ItemCode { get { return itemCode; } }
    [SerializeField] private string itemType;
    public string ItemType { get { return itemType; } }
    [SerializeField] private Sprite itemIcon;
    //Ŭ������ ���а��� 
    public Sprite ItemIcon { get { return itemIcon; } }
}
