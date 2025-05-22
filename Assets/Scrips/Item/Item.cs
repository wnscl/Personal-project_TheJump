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
    //클래스로 구분가능 

    public Action onUseEvent;

    public void UseingItem()
    {
        onUseEvent?.Invoke();
        //널이면 실행안함 널이 아니면 실행
        Destroy(this.gameObject);
    }
}
