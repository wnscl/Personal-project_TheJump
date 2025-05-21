using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

interface IUSEable
{
    void UseingItem();
}

public class Item : MonoBehaviour, IUSEable
{
    public string itemName;
    public int itemCode;

    public void UseingItem()
    {

    }
}
