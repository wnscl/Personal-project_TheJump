using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    public Text name;
    public Image image;
    public string itemName;
    public int itemCode;
    public string itemType;
    public Sprite itemIcon;
    //Ŭ������ ���а��� 

    public Action onUseEvent;

    public void UseingItem()
    {
        onUseEvent?.Invoke();
        //���̸� ������� ���� �ƴϸ� ����
        Destroy(this.gameObject);
    }
}
